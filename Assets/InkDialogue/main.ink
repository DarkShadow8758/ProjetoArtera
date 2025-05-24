//external functions
EXTERNAL StartQuest(questId)
EXTERNAL AdvanceQuest(questId)
EXTERNAL FinishQuest(questId)

//quest ids (quest id + "Id" for variable name)
VAR CollectCoinsQuestId = "CollectCoinsQuest"

//quest states (quest id + "State" for variable name) 
VAR CollectCoinsQuestState = "REQUIREMENTS_NOT_MET"

//-> collectCoinsStart.requirementsNotMet

//ink files
INCLUDE collect_coins_start_npc.ink
INCLUDE collect_coins_finish_npc.ink