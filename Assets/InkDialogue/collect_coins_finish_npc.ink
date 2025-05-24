=== collectCoinsFinish ===
{ CollectCoinsQuestState:
    - "FINISHED": -> finished
    - else: -> default
}

= finished
Muito obrigado!
-> END

= default
Hm? O que você quer?
* [Nada, foi um engano..]
    -> END
* { CollectCoinsQuestState == "CAN_FINISH"} [Eu estou com suas moeadas.]
    ~ FinishQuest(CollectCoinsQuestId)
    O que? Essas moedas são para mim? Muito obrigado!
-> END