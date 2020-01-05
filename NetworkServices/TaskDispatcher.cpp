#include "TaskDispatcher.h"
#include <memory> 
#include <ace/Thread.h> 
#include "TypeIds.h" 
#include "Stream.h"

namespace {

  using std::string;
  using Yammer::Task;
  using Yammer::Stream;
  using Yammer::NetworkError;

  // a sentinal
  class Stop : public Task {
  public:
    Task *clone() const { return new Stop; }
    void run() {}
    int type() const { return Yammer::InternalStopTaskId; }
    string name() const { return "InternalStopTask"; }
    void toStream(Stream &stream) const throw (NetworkError) {}
    void fromStream(Stream &stream) throw (NetworkError) {}
  } StopTask;

}
    
namespace Yammer {

  using std::auto_ptr;

  TaskDispatcher::TaskDispatcher(size_t queueDepth, size_t nthreads)
    : requestQueue_(queueDepth), nthreads_(nthreads) {}

  void TaskDispatcher::start()
  {
    for (;;) {
      Task *task;
      requestQueue_.pop(task);
      if (task == 0) continue;
      auto_ptr<Task> taskDeleter(task);
      if (*task == StopTask) break;
      task->run();
    }
  }

  void TaskDispatcher::stop() 
  {
    for (size_t i = 0; i < nthreads_; i++)
      requestQueue_.push(new Stop);
  }

  void TaskDispatcher::startThreadPool()
  {
    // default to number of processors online
    if (nthreads_ == 0) {
      size_t nprocessors = 1;//ACE_OS::sysconf(_SC_NPROCESSORS_ONLN);
      if (nprocessors == -1) nprocessors = 2;
      nthreads_ = nprocessors;
    }

    for (size_t i = 0; i < nthreads_; i++)
      ACE_Thread::spawn(run, this, THR_BOUND|THR_DETACHED);
  }

#ifdef _MSC_VER
 DWORD TaskDispatcher::run(void *This)
  {
    TaskDispatcher *that = reinterpret_cast<TaskDispatcher*>(This);
    that->start();
    return 0;
  }
#else
  void *TaskDispatcher::run(void *This)
  {
    TaskDispatcher *that = reinterpret_cast<TaskDispatcher*>(This);
    that->start();
    return 0;
  }
#endif
  void TaskDispatcher::dispatch(Task *task)
  {
    requestQueue_.push(task);
  }

  TaskDispatcher* get_task_dispatcher_instance()
	{
		return TaskDispatcherSingleton::instance();
	}
}