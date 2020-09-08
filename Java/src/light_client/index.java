package light_client;
import java.io.*;
import java.util.*;
import org.json.*;



public class index {
	
	public static BufferedReader inp;
	public static StringBuilder strbld = new StringBuilder();
    //To get this output
    public static void print(String responseStr) {
    	String[] strArray = responseStr.split(" ");
    	if(strArray[0].equals("Progress")) {
    		System.out.println("persentage : "+strArray[1].substring(0,strArray[1].length()-1));
    		System.out.println("downloaded : "+strArray[2].substring(1));
    		System.out.println("total_size : "+strArray[4].substring(0,strArray[4].length()-2));
    	}
    	else {
    		System.out.println(responseStr);
    	}
    }
    //valiadate Json
    private static boolean isJSONValid(String str) {
        try {
            new JSONObject(str);
        } catch (JSONException ex) {
            try {
                new JSONArray(str);
            } catch (JSONException ex1) {
                return false;
            }
        }
        return true;
    }
    //To return Json OutPut
    private static void printJson(String stream) {
    	//creating JSON and printing.
		strbld.append(stream.trim());
		if(isJSONValid(strbld.toString())) {
			print(strbld.toString());
			strbld = new StringBuilder();
		}
    }
   	public static void main(String[] args) {
		try
		{
			String stream;
			String path = "C:\\Users\\asuto\\eclipse-workspace\\ss-light-client-Java\\src\\light_client\\qa.exe"; //Give your file downloader binary path.
			String fileHash = "fzhnK3WkHNjxNfMgAtSeszKTGo"; // Give you file hash How to get file hash Vist:-https://developer.swrmlabs.io/#/?id=how-to-download-a-file-using-a-downloader
			String command = path+" -sharable "+fileHash+" -progress -json";
			Runtime run = Runtime.getRuntime();
			Process proc = run.exec(command);//executing the command
			inp = new BufferedReader( new InputStreamReader(proc.getInputStream()));//Creating Buffer Reader to get the exec input.
			//Reading the input Buffer and printing till Buffer Empty.
			
			while((stream = inp.readLine()) != null){
				

				//To get JOSN output make sure you are using -json in command variable
				printJson(stream);
				//To use non json output make sure you are not using -json in command variable
				//print(stream)
				
			}	     
	        inp.close();			
			proc.exitValue();
		}
		catch (Exception e) {
			System.out.println(e.getMessage());
		}
		
	}
}
