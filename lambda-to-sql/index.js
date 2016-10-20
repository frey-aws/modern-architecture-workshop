/*
*  Example: Connection to S3 bucket to grab file to load encrypted connection string and then run queries against a SQL Server
*/

var sql = require('mssql');
var async = require('async');
var NodeRSA = require('node-rsa');
var aws = require('aws-sdk');

loadData = function(result) {

    var s3 = new aws.S3();
    var params = {
        Bucket: "dfrey-sql",
        Key: "key.txt"
    };
    /* load key from S3 */
    s3.getObject(params, function(err, keyData) {
        if(err) {
            console.log(err);
        } else {
            params.Key = "sql.txt";

            /* load encrypted connection from S3 */            
            s3.getObject(params, function(err, data) {
                if(err) {
                    console.log(err);
                } else {

                    /* decrypt connection for SQL connection */
                    var key = new NodeRSA();
                    key.importKey(keyData.Body.toString());    
                    console.log(key.decrypt(data.Body.toString(), "utf8"));     
                    // sql.connect(key.decrypt(data.Body.toString(), "utf8")).then(function() {                                       
                    
                    /* get data from SQL server */
                    /*new sql.Request().query('select * from [Website].[Customers] Where [CustomerId] = 6')
                            .then(function(recordset) {
                                console.log(recordset);
                            }).catch(function(err) {
                                console.log(err);
                            });                 
                    }).catch(function(err) {
                        console.log(err);            
                    });*/                                    
                }
            });
        }
    });           
}

loadData();