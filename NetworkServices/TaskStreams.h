#ifndef _TASK_STREAMS_H_
#define _TASK_STREAMS_H_

#include <memory> 
#include "Task.h" 
#include "Stream.h" 
#include "NetworkExceptions.h"
#include "UnknownTask.h" 

#ifdef _MSC_VER
#pragma warning( disable : 4290 )
#endif

namespace Yammer {

  using std::auto_ptr;

  // stream I/O operators for Task objects

  MYLIB_DLLAPI Stream &operator>>(Stream&, Task*&) throw (UnknownTask, NetworkError);

  MYLIB_DLLAPI Stream &operator<<(Stream&, const Task*) throw (NetworkError);


  // helper functions are for syntax only, semantics are the same
  MYLIB_DLLAPI Stream &operator>>(Stream&, std::auto_ptr<Task>&)
    throw (UnknownTask, NetworkError);

  MYLIB_DLLAPI Stream &operator<<(Stream&, const Task&) throw (NetworkError);

  MYLIB_DLLAPI Stream &operator<<(Stream&, auto_ptr<Task>&) throw (NetworkError);

}

#endif