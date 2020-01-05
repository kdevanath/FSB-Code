#ifndef __LOG_REQUEST_H__
#define __LOG_REQUEST_H__

#include <iostream>
#include "NetworkServicesExport.h"
#include <Data/LogData.h>
#include <Utilities/FSBMsgsId.h>

#include <NetworkServices/Stream.h>
#include <NetworkServices/Task.h>
#include <NetworkServices/TaskSync.h>



using Yammer::Stream;
using Yammer::Task;
using Yammer::TaskSync;
using Yammer::NetworkError;

namespace fsb {

	class MYLIB_DLLAPI LogMARequest : public Yammer::Task
	{
	public:
		LogMARequest();
		LogMARequest* clone() const { return new LogMARequest(*this); }

		void run() { }

		int type() const { return fsb::LogMARequestId; }
		std::string name() const { return "LogMARequest";}

		void toStream(Stream& stream) const throw (NetworkError);
		void fromStream(Stream& stream) throw (NetworkError);

		void sync(TaskSync *sync) { sync_ = sync; }
		bool needsReply() const { return false; }

		void addMA(const LogMA& l);

		LogMA _maInfo;

	private:
		TaskSync *sync_;
		LogMA::Vect _maInfos;
	};

	
	class LogRequest : public Yammer::Task
	{
	public:
		LogRequest();
		LogRequest* clone() const { return new LogRequest(*this); }

		void run() { }

		int type() const { return fsb::LogRequestId; }
		std::string name() const { return "LogRequest";}

		void toStream(Stream& stream) const throw (NetworkError);
		void fromStream(Stream& stream) throw (NetworkError);

		void sync(TaskSync *sync) { sync_ = sync; }
		bool needsReply() const { return false; }

		void addLogData(const RTDataLog& l);

		RTDataLog logInfo;

	private:
		TaskSync *sync_;
		RTDataLog::Vect _rtDataLogs;
	};
}
#endif
