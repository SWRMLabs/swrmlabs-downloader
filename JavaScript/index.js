const { Observable, Subject } = require("rxjs");
const { exec } = require("child_process");
const path = require('path')
let cliPath = path.join(__dirname, 'qa.exe')

function promiseFromChildProcess(child) {
    return new Promise(function (resolve, reject) {
      child.addListener("error", reject);
      child.addListener("exit", resolve);
    });
}
  
function downloadHash(hash){
    cmd = `${cliPath} -sharable ${hash} -progress`;
    console.log("cmd", cmd);
    let child = exec(cmd);
    return new Observable((subscriber) => {
        promiseFromChildProcess(child).then(
        function(result) {
            console.log("promise complete: " + result);
        },
        function(err) {
            console.log("promise rejected: " + err);
        }
        );
        child.stdout.on("data", async function(data) {
            subscriber.next(data);
        });
    });
}

function run (){
    return new Promise((resolve, reject)=>{
        const hash = process.argv[2]
        console.log("a fzhnMWQ5feB842R6pQa2kTPzMo", process.argv[2])
        let data = downloadHash(hash)
        let subscription = data.subscribe((res) => {
            var data = res.slice(8,18);
            console.log(data)
            resolve(data)
        });
    })
}

run()