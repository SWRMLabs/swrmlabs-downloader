const { Observable, Subject } = require("rxjs");
const { exec } = require("child_process");
const path = require('path')
let cliPath = path.join(__dirname, 'qa.exe')

/**
 * addListener for Observable
 * @method promiseFromChildProcess
 * @params {child}
 */
function promiseFromChildProcess(child) {
    return new Promise(function (resolve, reject) {
      child.addListener("error", reject);
      child.addListener("exit", resolve);
    });
}
  
 /**
 * calling the cli command and Observing the output and returning to run()
 * @method downloadHash
 * @params {hash}
 */ 
function downloadHash(hash){
    // calling command and passing hash
    cmd = `${cliPath} -sharable ${hash} -progress`;
    console.log("cmd", cmd);
    //exec to execute command in JS
    let child = exec(cmd);
    //Observable to check data stream
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

/**
 * subscribe the data values for display the downloaded progress 
 * @method run
 */
function run (){
    return new Promise((resolve, reject)=>{
        // taking hash as a argv when calling node index .js <HASH>
        const hash = process.argv[2]
        console.log("a fzhnMWQ5feB842R6pQa2kTPzMo", process.argv[2])
        let data = downloadHash(hash)
        // subscribing the data values
        let subscription = data.subscribe((res) => {
            var data = res.slice(8,18);
            console.log(data)
            resolve(data)
        });
    })
}

// default function call when execute node index.js
run()