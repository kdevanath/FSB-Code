#ifndef _YAMMER_MESSAGE_QUEUE_H_
#define _YAMMER_MESSAGE_QUEUE_H_
#include <iostream>
#include <time.h>
#include <ace/Synch.h>
#include <ace/Guard_T.h>
#include <ace/Condition_T.h>
#include <ace/Synch_Traits.h>
//#include <ace/Synch_T.h> // Using ACE 5.3
#include <queue>


#ifdef _MSC_VER
#pragma warning(disable:4267)
#include "NetworkServicesExport.h"
#endif

namespace Yammer {

  using std::queue;

  // A synchronized, bounded queue
  template <typename T> 
  class MYLIB_DLLAPI MessageQueue {
  public:
    enum { DEFAULT_QUEUE_DEPTH = 128 };

    MessageQueue(size_t sz = DEFAULT_QUEUE_DEPTH) : sema_(sz), cond_(mutex_) {}

    void push(const T &msg)
    {
	/*	if( queue_.size() > DEFAULT_QUEUE_DEPTH) 
			std::cout << " Will block " << std::endl;*/
      sema_.acquire();  // block if full

      ACE_Guard<ACE_Thread_Mutex> guard(mutex_);
      queue_.push(msg);
      cond_.signal();
    }

    void pop(T &msg)
    {
      ACE_Guard<ACE_Thread_Mutex> guard(mutex_);

      while (queue_.empty()) 
        cond_.wait();  // block if empty

      msg = queue_.front();
      queue_.pop();

      guard.release();
      sema_.release();
    }

    bool pop(T &msg, long timeout)
    {
      ACE_Guard<ACE_Thread_Mutex> guard(mutex_);

      if (queue_.empty()) {
        ACE_Time_Value millisecs(time(0) + timeout / 1000, 
          timeout % 1000 * 1000);  // milliseconds -> microseconds
        cond_.wait(&millisecs);
        if (queue_.empty())
          return false;  // timed out
      }

      msg = queue_.front();
      queue_.pop();

      guard.release();
      sema_.release();
      return true;
    }

    bool empty() 
    {
      ACE_Guard<ACE_Thread_Mutex> guard(mutex_);
      return queue_.empty();
    }
      
  private:
    ACE_Semaphore sema_;
    ACE_Thread_Mutex mutex_;
    ACE_Condition<ACE_Thread_Mutex> cond_;
    queue<T> queue_;
  };
}

#endif
