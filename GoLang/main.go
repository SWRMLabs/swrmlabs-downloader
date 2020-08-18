package main

import (
	"bufio"
	"bytes"
	"flag"
	"fmt"
	"log"
	"os/exec"
	"strings"
	"sync"
	"time"
)

// Command arguments
var (
	bin = flag.String("bin", "ss-light-linux-amd64", "Location of lite-client")
	// Provide hash/    sharable string
	sharable = flag.String("sharable", "", "Sharable string provided for file")
)

/**
 * Driver function -
 * @method main
 * @params {null}
 */
func main() {

	// Parse command arguments
	flag.Parse()
	if len(*sharable) == 0 {
		fmt.Println(`
    Usage:
        ./client <OPTIONS>

    Options:
            `)
		flag.PrintDefaults()
		log.Fatal("Sharable string not provided")
	}

	// Prepare command with exec
	var waitGroup sync.WaitGroup
	cmd := exec.Command(*bin, "-sharable", *sharable, "-progress")
	eOut := new(bytes.Buffer)
	cmd.Stderr = eOut
	stdout, err := cmd.StdoutPipe()
	if err != nil {
		log.Fatal("command failed ", err.Error())
	}
	rd := bufio.NewReader(stdout)

	// Channel to stop progress routine
	done := make(chan bool)

	// Wait for progress to be done
	waitGroup.Add(1)
	go func() {
		defer waitGroup.Done()
		for {
			str, _, err := rd.ReadLine()
			if err != nil {
				log.Println("Read Error:", err)
			}
			sar := strings.Split(string(str), " ")
			fmt.Println("Progress :", sar[len(sar)-1])
			select {
			case <-time.After(time.Millisecond * 500):
			case <-done:
				close(done)
				return
			}
		}
	}()

	// Run command
	err = cmd.Run()
	if err != nil {
		done <- true
		log.Fatal("command failed ", err.Error(),
			string(eOut.Bytes()))
	}

	// Signal done
	done <- true
}
