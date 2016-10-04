//---------------------------------
// University of Central Florida
// CIS3360 - Fall 2016
// Program Author: Daved Sanchez
//---------------------------------

#include <iostream>
#include <string>
#include <fstream>
#include <ctype.h>
#include <stdlib.h>
#include <vector>

using namespace std;

const int MAX_LEN = 5000;
const int MAX_CHAR = 80;

void viewFile(char* fileName);
void printVec(vector<int> vec);
void format(string text);

int reduce(int num);
int charToInt(char a);

vector<int> parseStringToVector(string block);
vector<int> add(vector<int> block, vector<int> key);
vector<int> modulo(vector<int> vec);

char intToChar(int num);

string ripFile(char* fileName);
string splitText(int remainLen, int locTracker, string plainText, string key, int& padsAdded);
string addPads(string plainText, int padCount, int len, int& padsAdded);
string parseVectorToString(vector<int> vec);
string encrypt(vector<int> plain, vector<int> key, vector<int> mainKey);

int main(int argc, char** argv){
	const string plainText = ripFile(argv[1]);
	const string key = argv[2];
	const string initVec = argv[3];
	
	string plainBlock, cipText, finalText;

	int locTracker = 0, remainLen = plainText.length(), firstRun = 1, padsAdded = 0;
	
	//program info
	cout << "CBC Vigenre by Daved Sanchez" << endl;
	cout << "Plaintext file name: " << argv[1] << endl;
	cout << "Vignere keyword: " << key << endl;
	cout << "Initilization vector: " << initVec << endl << endl;
	
	//plainText output with formatting
	cout << "Clean Plaintext:" << endl << endl;
	format(plainText);
	cout << endl << endl;
	
	//cip Text header
	cout << "Ciphertext:" << endl << endl;
	
	//run ripping and encryption till the end
	while(remainLen > 0){
		//rip chunks the size of the key word from the plainText string
		plainBlock = splitText(remainLen, locTracker, plainText, key, padsAdded);
		
		//used to assert that the first run through vector is the init vector
		if(firstRun){
			cipText = initVec;
			firstRun = 0;		
		}
		
		//assign newly encrypted chunk to cipText, then contatinate cipText (block) to overall finalText
		cipText = encrypt(parseStringToVector(plainBlock), parseStringToVector(cipText), parseStringToVector(key));
		finalText += cipText;		
		
		//keep track how far down the plainText we are, or how much we have already encryped? Interprit as you wish
		//also used to check if we have went over/hit the 80 char per line limit
		locTracker += key.length();

		//used to make sure we will exentually exhaust the enitre plainText
		remainLen -= key.length();
	}
	
	//format the cipher Text in the 80 char/line
	format(finalText);
	
	//stats section
	cout << endl << endl;
	cout << "Number of characters in clean plaintext file: " << plainText.size() << endl;
	cout << "Block size = " << key.size() << endl;
	cout << "Number of pad characters added: " << padsAdded << endl << endl;

	return 0;
}

//utility fun to ensure the file was being opened
void viewFile(char* fileName){
	string line;
	ifstream file(fileName);
	if(file.is_open()){
		while(getline(file, line)){
			cout << line << '\n';
		}
	}

	file.close();
}

//load in the file and rip its contents into a usable string
//fileName will be argv[1]
//returns usable string to encryption
string ripFile(char* fileName){
	char parsedText[5000];
	int loadChar = 0;
	int currentChar = 0;
		
	//rip line by like, then rip each char and place into string
	string line;
	ifstream file(fileName);
	if(file.is_open()){
		while(getline(file, line)){
			for(int i = 0; i < line.length(); i++){
				if(isalpha(line[i])){
					parsedText[loadChar] = tolower(line[i]);
					loadChar++;
				}
					
			}
		}
	file.close();
	}else
		cout << "File Not Found" << endl;
	return parsedText;
}

//turns chars to ints
int charToInt(char a){
	return a - 'a';
}

//turns ints to chars
char intToChar(int num){
	return num + 'a';
}

//will reduce incoming num (will be checked if > 26)
//will return the reduced number so it is below 26 (the remainder)
int reduce(int num){
	return num - 26;
}

//takes the plainText and 'pulls' and returns a chunch to encrypt
//int& padsAdded is a pass by reference var that will enable assignment of the number of pads
//added without extra calulation, re-calulate, or global vars
string splitText(int remainLen, int locTracker, string plainText, string key, int& padsAdded){
	string plainBlock;
	int padCount = 0;	

	for(int charRipper = 0; charRipper < key.length(); charRipper++){
		//when (if) it come to needing pads, add the pads			
		if(remainLen < key.length()){	
			//found this with ALGEBRA, so convenient the 'formula' was simple			
			padCount = -1*(remainLen - key.length());				
			plainBlock = addPads(plainText, padCount, remainLen, padsAdded);
			break;
		}
		plainBlock += plainText[locTracker + charRipper];
	}

	return plainBlock;
}

//exhausts the rest of the plainText into a string holder, then
//the remaining slots are padded with 'x'
//	where padsAdded is assigned is a pass by ref var
string addPads(string plainText, int padCount, int len, int& padsAdded){
	char temp[MAX_LEN];
	
	//iterate through string block till the last one
	for(int i = 0; i < len; i++)
		temp[i] = plainText[plainText.length() - len + i];
	
	//add the needed number of pads
	for(int i = len; i < padCount + len; i++)
		temp[i] = 'x';
	
	//assigned number of padsAdded to the mount of pads that were added
	padsAdded = padCount;

	return temp;
}

//convert string to int vectors, vectors are more convenient than arrays
vector<int> parseStringToVector(string block){
	vector<int> vec;		
	
	for(int i = 0; i < block.length(); i++)
		vec.push_back(charToInt(block[i]));
	
	return vec;
}

//add two vectors, quit literally and for the purpose of this encryption program it being an arry of ints
vector<int> add(vector<int> block, vector<int> key){
	vector<int> result;	

	for(int i = 0; i < block.size(); i++)
		result.push_back(block[i] + key[i]);

	return result;
}

//simple say and make all elemtns of the vectors be less than 26
vector<int> modulo(vector<int> vec){
	int i = 0;
	while(i < vec.size()){
		if(vec[i] >= 26)
			vec[i] = reduce(vec[i]);
		else
			i++;
	}	

	return vec;
}

//converts a vector (literal numbers) into a string so it may be read
string parseVectorToString(vector<int> vec){
	char temp[MAX_LEN];
	
	for(int i = 0; i < vec.size(); i++)
		temp[i] = intToChar(vec[i]);

	return temp;
}

//str8 4-ward encryption, just call the functions and pass stuff in
//helped a lot to work it out on paper with all the switching
string encrypt(vector<int> plain, vector<int> key, vector<int> mainKey){
	//cbc pass	
	key = modulo(add(plain, key));
	//vigenere pass
	return parseVectorToString(modulo(add(key, mainKey)));
}

//formatter to out put only 80 chars/line
void format(string text){
	int i = 0;
	while(text[i] != '\0'){
		cout << text[i];
		i++;
		if(i%80 == 0)
			cout << endl;
	}
}

//utility function to help debug and see where (and how) the addition was going
void printVec(vector<int> vec){
	for(int i = 0; i < vec.size(); i++)
		cout << vec[i] << " ";

	cout << endl;
}
