var sql = require('mssql');
var NodeRSA = require('node-rsa');
var fs = require('fs');

loadData = function() {

    var connDecrypt;

    fs.readFile("credential/key.txt", 'utf8', function(err, data) {
        console.log(data);
        var key = new NodeRSA();
        key.importKey(data);
        fs.readFile("credential/sql.txt", "utf8", function(err, connEncrypt) {            
            connDecrypt = key.decrypt(connEncrypt, "utf8");

            console.log(connDecrypt);
        });
             
        
        // console.log(conn);
        /*
        new sql.Request().query('select * from mytable')
                    .then(function(recordset) {
                            console.dir(recordset);
                    }).catch(function(err) {
                        // potentially console the error
                    });
        */    
    });
}

loadData();

