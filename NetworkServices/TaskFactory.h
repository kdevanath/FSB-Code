#ifndef _YAMMER_TASK_FACTORY_H_
#define _YAMMER_TASK_FACTORY_H_
#include "Task.h" 
#include <ace/Singleton.h> 
#include <ace/Synch.h> 
#include <Factory/Factory.h> 

#include "UnknownTask.h" 
#include <iostream>

using namespace fsbfactory;

namespace Yammer {

	class MYLIB_DLLAPI TaskFactory : public Factory<int, Task> {
	public:
		Task *createTask(Stream &stream) throw (UnknownTask, NetworkError) {
			//printprototypeFactory();
			int type;
			stream >> type;
			//std::cout << " type " << type << __FILE__ << std::endl;
			try { return create(type); }
			catch (UnknownType) { std::cout << " type " << type << __FILE__ << std::endl; throw UnknownTask(); }
		}
	};

	typedef ACE_Singleton<TaskFactory, ACE_Thread_Mutex> TaskFactorySingleton;

	// To automatically register a task with TaskFactory, create a global instance
	// of TaskRegistrar.  For example, the following line can be placed in the .C
	// file of the Task class.
	//
	//   TaskRegistrar<MyTask> MyTaskPrototype;

	template <typename TaskClass>
	struct TaskRegistrar {
		TaskRegistrar() {
			TaskClass *prototypicalInstance = new TaskClass;
			//std::cout << " Type in factory " << prototypicalInstance->type() << std::endl;
			//TaskFactorySingleton::instance()->
			get_instance_taskfactory()->
				registerPrototype(prototypicalInstance->type(), prototypicalInstance);
		}
	};

#define REGISTER_TASK(TaskClass)                                        \
	namespace {                                                             \
	struct TaskRegistrar {                                                \
	TaskRegistrar() {                                                   \
	TaskClass *prototypicalInstance = new TaskClass;                  \
	typedef Yammer::TaskFactorySingleton TF;            \
	TF::instance()->registerPrototype(prototypicalInstance->type(),   \
	prototypicalInstance);          \
	}                                                                   \
	} TaskRegistrarInstance;                                              \
	} 

	MYLIB_DLLAPI TaskFactory* get_instance_taskfactory();
}

#endif