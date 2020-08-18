package light_client;
import java.io.*;
import java.util.*;


public class index {
	
	public static BufferedReader inp;
    public static BufferedWriter out;
    
    // Parsing the Buffer and printing To console.
    public static void print(String s) {
    	s = s.substring(0,s.length()-1);
		s= s.substring(10);
    	System.out.println(s);
    }
    //This function to read the out Buffer and return the result.
    public static String pipe(String msg) {
    	String ret;
    	try {
			out.write(msg + '\n');
			out.flush();
			ret = inp.readLine();
			return ret;
		} catch (Exception e) {
			System.out.println(e.getMessage());
		}
    	return "";
    }
	public static void main(String[] args) {
		try
		{
			String stream;
			String path = "PathTolightClient"; //Give your file downloader binary path.
			String fileHash = "fzhnMWQ5feB842R6pQa2kTPzMo"; // Give your file hash. How to get file hash Vist:-https://developer.swrmlabs.io/#/?id=how-to-download-a-file-using-a-downloader
			String command = path+" -sharable "+fileHash+" -progress";
			Runtime run = Runtime.getRuntime();
			Process proc = run.exec(command);//executing the command
			
			inp = new BufferedReader( new InputStreamReader(proc.getInputStream()));//Creating Buffer Reader to get the exec input.
			out = new BufferedWriter( new OutputStreamWriter(proc.getOutputStream()));//Creating Buffer Writer to write the stdout.
			
			//Reading the input Buffer and printing till Buffer Empty.
			while((stream = inp.readLine()) != null){
				print(pipe(stream));
			}
	     
			//closing the pipe and Buffers.
	        pipe("quit");
	        inp.close();
	        out.close();
			
			
		}
		catch (Exception e) {
			System.out.println(e.getMessage());
		}
		
	}
}
