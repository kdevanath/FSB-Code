//#include <memory>
#include <string.h>
#include "FileStreamAdapter.h"
#include <Utilities/FSBLogger.h>

namespace Yammer {

	FileStreamAdapter::FileStreamAdapter() { std::cout << " I should not be here" << std::endl;}

	FileStreamAdapter::FileStreamAdapter(const std::string& fname)
		:_fname(fname)
	{
		std::cout << " Opening file " << fname << std::endl;
		_file.open(fname.c_str(),std::ios_base::out|std::ios_base::app|std::ios_base::binary);
		if( !_file.is_open() ) {
			std::cout << " problem opening to read the file" << std::endl;
			throw FileOpenFailed();
		}
		_input.open(fname.c_str(),std::ios_base::in|std::ios_base::binary);
	}

	int FileStreamAdapter::read(void *buffer, size_t len)
	{
		if(_input.is_open()) {
			if(!_input.eof() ) {
				_input.read(reinterpret_cast<char *>(buffer),len);
				if(_input.fail()) {
					std::cout << " did reach now " << std::endl;
					//_input.close();
					return -1;
					//throw ReachedEndOfFile();
				}
			} else
				return -1;

			return len;
		}
		return -1;
	}

	int FileStreamAdapter::read(void *&buffer, size_t len)
	{
		std::cout << " I should not be here " << std::endl;
		if(_file.is_open() ) {
			FSB_LOG("Close it and repoen it for reading");
			_file.close();
			 _file.open(_fname.c_str(),ios::in|ios::binary|ios::ate);
			if(!_file.is_open() ) {
				FSB_LOG("Could not open the file to read, will reopen to write");
				_file.open(_fname.c_str(),std::ios_base::out|std::ios_base::binary|std::ios_base::ate);
				return 0;
			}
		}
	
		ifstream::pos_type size = _file.tellg();
		std::cout  << " size of the file " << size << std::endl;
		if( (int)size==0){
			FSB_LOG("May be beginning of the day, nothing to see");
			_file.close();
			_file.open(_fname.c_str(),std::ios_base::out|std::ios_base::binary|std::ios_base::app);
			return 0;
		}

		char* ptr = reinterpret_cast< char* >(buffer);
		try {
			std::auto_ptr<char> readBuffer(new char[size]);
			memset(readBuffer.get(),0,size);
			_file.read(readBuffer.get(),size);	
			ptr = readBuffer.release();
		}catch(std::bad_alloc e)
		{
			FSB_LOG("ERROR: The file may be too big, truncate the file " << size);
			size=0;
		}	
		_file.close();
		_file.open(_fname.c_str(),std::ios_base::out|std::ios_base::binary|std::ios_base::app);
		return size;
	}

	int FileStreamAdapter::write(const void *buffer, size_t len)
	{
		int numberOfBytesWritten=0;
		_file.write(reinterpret_cast<const char *>(buffer),len);
		return numberOfBytesWritten;
	}

	void FileStreamAdapter::close()
	{
		_file.close(); 
	}
}