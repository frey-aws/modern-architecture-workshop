var NodeRSA = require('node-rsa');
var fs = require('fs');
var key = new NodeRSA({b: 2048});

var text = "mssql://www.user:aUXxafvqQ5ntpbNZ23CkzXbv@172.20.6.223/WideWorldImporters";

// console.log(key);
var encrypted = key.encrypt(text, 'base64', 'utf8');
fs.writeFile('../credential/sql.txt', encrypted, function() {    
    // console.log('Decrypted:' + decrypted);
    fs.writeFile('../credential/key.txt', key.exportKey("private"), function() {});
});
