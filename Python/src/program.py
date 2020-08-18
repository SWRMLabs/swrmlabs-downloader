from subprocess import Popen, PIPE, CalledProcessError

binName = 'qa.exe'
fileHash = 'fzhnK3WkHNjxNfMgAtSeszKTGo'

args = (binName+' -sharable '+fileHash+' -progress')



with Popen(args, stdout=PIPE, bufsize=1, universal_newlines=True) as p:
    for b in p.stdout:
        print(b.split(" ")[1], end='') # b is the byte from stdout

if p.returncode != 0:
    raise CalledProcessError(p.returncode, p.args)
