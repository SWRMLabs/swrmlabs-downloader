from subprocess import Popen, PIPE, CalledProcessError
import json

binName = 'qa.exe' # Give your file downloader binar path
fileHash = 'fzhnpPQMEbR8Gz9xPfpdk71P4Q' # Give your file hash. How to get file hash Vist:-https://developer.swrmlabs.io/#/?id=how-to-download-a-file-using-a-downloader

args = (binName+' -sharable '+fileHash+' -progress')

#function to pash json
def parseJosnOutput(stringObject):
    jsonObject = json.dumps(stringObject)
    jsonObject1 = json.loads(jsonObject)
    return jsonObject

#function to convert non json output to json.
def pasreNonJsonOutput(stringObject):
    if stringObject[0] == "P":
        stringArr = stringObject.split(" ");
        return { "percentage":stringArr[1][0],"downloaded":stringArr[2][1:],"size":stringArr[4][:-1] }
    else:
        return stringObject
# //executing the command and assign to p
with Popen(args, stdout=PIPE, bufsize=1, universal_newlines=True) as p:
    for b in p.stdout: #reading the stdout and printing
        print(pasreNonJsonOutput(b))

if p.returncode != 0: 
    raise CalledProcessError(p.returncode, p.args)