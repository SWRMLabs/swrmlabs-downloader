package main

import (
	"bytes"
	"encoding/json"
	"flag"
	"fmt"
	"io"
	"log"
	"os/exec"
	"sync"
)

type Out struct {
	Status  int         `json:"status"`
	Message string      `json:"message"`
	Data    json.RawMessage `json:"data,omitempty"`
	Details string      `json:"details,omitempty"`
}

type ProgressOut struct {
	Percentage int    `json:"percentage"`
	Downloaded string `json:"downloaded"`
	TotalSize  string `json:"total_size"`
}

// Command arguments
var (
	bin = flag.String("bin", "ss-light-linux-amd64", "Location of lite-client")
	sharable    = flag.String("sharable", "", "Sharable string provided for file")
)

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
	cmd := exec.Command(*bin, "-sharable", *sharable, "-progress", "-json")
	eOut := new(bytes.Buffer)
	cmd.Stderr = eOut
	stdout, err := cmd.StdoutPipe()
	if err != nil {
		log.Fatal("command failed ", err.Error())
	}
	// Wait for progress to be done
	waitGroup.Add(1)
	go func() {
		defer waitGroup.Done()
		dec := json.NewDecoder(stdout)
		for {
			jsonOut := &Out{}
			err := dec.Decode(&jsonOut)
			if err == io.EOF {
				break
			}
			if err != nil {
				log.Fatal("unable to parse json data : ", err.Error())
			}
			if jsonOut.Message == "Progress" {
				progressOut := &ProgressOut{}
				err := json.Unmarshal(jsonOut.Data, progressOut)
				if err != nil {
					log.Fatal("unable to parse progress output ", err.Error())
				}
				fmt.Println(fmt.Sprintf("Progress %d%%", progressOut.Percentage))
			}
		}
	}()

	// Run command
	err = cmd.Run()
	if err != nil {
		log.Fatal("command failed ", err.Error(),
			string(eOut.Bytes()))
	}
}