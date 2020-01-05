#include "TaskStreams.h"
#include "TaskFactory.h" 

namespace Yammer {

  Stream &operator>>(Stream &stream, Task *&task) 
    throw (UnknownTask, NetworkError)
  { 
    task = TaskFactorySingleton::instance()->createTask(stream);
    task->fromStream(stream);
    return stream; 
  }
    
  Stream &operator<<(Stream &stream, const Task *task) throw (NetworkError)
  { 
    task->toStream(stream);
    return stream; 
  }

  Stream &operator>>(Stream &stream, auto_ptr<Task> &task)
    throw (UnknownTask, NetworkError)
  { 
	  //std::cout << " Am in auto_ptr? " << std::endl;
    Task *aTask;
    stream >> aTask; 
    task.reset(aTask);  // deletes old pointer 
    return stream;
  }

  Stream &operator<<(Stream &stream, const Task &task) throw (NetworkError)
  { 
    return stream << &task; 
  }

  Stream &operator<<(Stream &stream, auto_ptr<Task> &task) throw (NetworkError)
  { 
    return stream << task.get(); 
  }

}

