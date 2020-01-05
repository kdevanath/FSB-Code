#include "ServiceFactory.h"

namespace Yammer {

	ServiceFactory* get_service_factory() 
	{
		return ServiceFactorySingleton::instance();
	}
}