var NodeRSA = require('node-rsa');
var fs = require('fs');

fs.readFile("sql.txt", function(err, text) {
    var key = new NodeRSA({b: 2048});
    var encrypted = key.encrypt(text, 'base64', 'utf8');
    fs.writeFile('../credential/sql.txt', encrypted, function() {        
        fs.writeFile('../credential/key.txt', key.exportKey("private"), function() {});
    });
})