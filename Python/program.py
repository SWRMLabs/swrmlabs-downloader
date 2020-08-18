from subprocess import Popen, PIPE, CalledProcessError

# Give your file downloader binar path
binName = 'pathOfLightClient' 
# Give your file hash. How to get file hash Vist:-https://developer.swrmlabs.io/#/?id=how-to-download-a-file-using-a-downloader
fileHash = 'fzhnMWQ5feB842R6pQa2kTPzMo' 

args = (binName+' -sharable '+fileHash+' -progress')

# //executing the command and assign to p
with Popen(args, stdout=PIPE, bufsize=1, universal_newlines=True) as p:
    for b in p.stdout: #reading the stdout and printing
        print(b.split(" ")[1], end='') # b is the byte from stdout

if p.returncode != 0: 
    raise CalledProcessError(p.returncode, p.args)