#ifndef _YAMMER_FILE_STREAM_ADAPTER_H_
#define _YAMMER_FILE_STREAM_ADAPTER_H_

#include <iostream> 
#include <fstream>
#include "Stream.h"

namespace Yammer {

	class MYLIB_DLLAPI FileStreamAdapter : public Stream {  // Adapter pattern

	public:
		class FileOpenFailed {};
		class ReachedEndOfFile{};

		FileStreamAdapter();
		~FileStreamAdapter(){ flush(); _file.close();}
		FileStreamAdapter(const std::string& fname);

		int read(void *&buffer, size_t len) throw (NetworkError);
		int read(void *buffer, size_t len) throw (NetworkError);
		int write(const void *buffer, size_t len) throw (NetworkError);

		int readv(const iovec *vec, int len) throw (NetworkError) { return 0;}
		int writev(const iovec *vec, int len) throw (NetworkError) { return 0;}

		void flush() {_file.flush(); }
		void close();
		int handle() { return 0; }

	private:
		fstream _file;
		ifstream _input;
		std::string _fname;


	};
}
#endif
