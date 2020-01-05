#include "MsgHelper.h" 
#include "ByteOrder.h" 

namespace Yammer {

 vector<char> &operator<<(vector<char> &buffer, char val)
  {
	//std::cout << " char? " << val << std::endl;
    buffer.push_back(val);
    return buffer;
  }

  vector<char> &operator<<(vector<char> &buffer, bool val)
  {
	  //std::cout << " " << val << std::endl;
    buffer.push_back(val);
    return buffer;
  }

  vector<char> &operator<<(vector<char> &buffer, int val)
  {
	  //std::cout << " " << val << std::endl;
    hton(val);
    char *p = reinterpret_cast<char*>(&val);
    buffer.insert(buffer.end(), p, p + word);  // append
    return buffer;
  }

  vector<char> &operator<<(vector<char> &buffer, long val)
  {
	  //std::cout << " " << val << std::endl;
    hton(val);
    char *p = reinterpret_cast<char*>(&val);
    buffer.insert(buffer.end(), p, p + word);  // append
    return buffer;
  }

  vector<char> &operator<<(vector<char> &buffer, size_t val)
  {
	  //std::cout << " " << val << std::endl;
    hton(val);
    char *p = reinterpret_cast<char*>(&val);
    buffer.insert(buffer.end(), p, p + word);  // append
    return buffer;
  }

  vector<char> &operator<<(vector<char> &buffer, double val)
  {
	  //std::cout << " " << val << std::endl;
    hton(val);
    char *p = reinterpret_cast<char*>(&val);
    buffer.insert(buffer.end(), p, p + dword);  // append
    return buffer;
  }

  vector<char> &operator<<(vector<char> &buffer, const std::string &val)
  {
	  //std::cout << " " << val << std::endl;
    size_t size = val.size();
    const char *p = val.c_str();
    buffer << size;  // append size
    if (size <= 0) return buffer;
    buffer.insert(buffer.end(), p, p + size);  // and string
    return buffer;
  }

}