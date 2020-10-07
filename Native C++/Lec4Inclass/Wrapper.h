#pragma once
#ifndef __WRAPPER__
#define __WRAPPER__

#include "PluginSettings.h"
#include "CheckpointTimeLogger.h"

#ifdef __cplusplus
extern "C" {
#endif

	// my functions here

	PLUGIN_API void ResetLogger();
	PLUGIN_API void SaveCheckpointTime(float RTBC);
	PLUGIN_API float GetTotalTime();
	PLUGIN_API float GetCheckpointTime(int index);
	PLUGIN_API int GetNumCheckpoints();

#ifdef __cplusplus
}
#endif

#endif /* defined(__WRAPPER__) */

class Wrapper {
};

