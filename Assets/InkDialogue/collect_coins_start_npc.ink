=== collectCoinsStart ===
{ CollectCoinsQuestState :
    - "REQUIREMENTS_NOT_MET": -> requirementsNotMet
    - "CAN_START": -> canStart
    - "IN_PROGRESS": -> inProgress
    - "CAN_FINISH": -> canFinish
    - "FINISHED": -> finished
    - else: -> END
}

= requirementsNotMet
//not possible for this quest, but putting something here anyways
Você ta muito fraquinho, vai lá comer xp antes de falar comigo!
-> END

= canStart
Ei Você!
Pode coletar 5 moedas e entregá-las para meu amigo?
* [Sim] //Posso com certeza!
    ~ StartQuest(CollectCoinsQuestId)
    Valeu!
* [Não] //Não estou afim..
    //Ah, tudo bem, fica ai lesando então!
- -> END

= inProgress
Está conseguindo pegar as moedas?
-> END

= canFinish
Pera? O que? Você pegou todas elas?
Vai lá falar com ele, o que está esperando??
-> END

= finished
Muito obrigado por ajudar meu amigo hehe!
-> END