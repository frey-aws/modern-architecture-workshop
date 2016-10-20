var NodeRSA = require('node-rsa');
var fs = require('fs');
var key = new NodeRSA({b: 2048});

var text = "mssql://www.user:aUXxafvqQ5ntpbNZ23CkzXbv@52.0.217.249:50002/WideWorldImporters";

var encrypted = key.encrypt(text, 'base64', 'utf8');
fs.writeFile('../credential/sql.txt', encrypted, function() {        
    fs.writeFile('../credential/key.txt', key.exportKey("private"), function() {});
});