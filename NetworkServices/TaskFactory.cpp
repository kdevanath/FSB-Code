#include "TaskFactory.h"

namespace Yammer {

	TaskFactory* get_instance_taskfactory() 
	{
		return TaskFactorySingleton::instance();
	}
}