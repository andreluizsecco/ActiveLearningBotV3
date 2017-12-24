# ActiveLearningBot
Chatbot utilizando o conceito de Active Learning com o LUIS (Microsoft Cognitive Services)

## Active Learning
> "O aprendizado ativo é um caso especial de aprendizagem em máquina semi-supervisionada em que um algoritmo de aprendizagem é capaz de interagir interativamente o usuário (ou alguma outra fonte de informação) para obter as saídas desejadas em novos pontos de dados." (Wikipedia)

## O Projeto
O projeto é um chatbot simples para exemplificar o conceito de active learning aplicado a bots com o uso do [LUIS](http://luis.ai).
Para tal, o mesmo faz uso do SDK [Cognitive.LUIS.Programmatic](https://www.nuget.org/packages/Cognitive.LUIS.Programmatic) que permite criar novas intenções, entidades, treinar seu modelo e publicá-lo de forma programática.

O SDK está disponível no Nuget e em um projeto Open Source no GitHub:
* [Cognitive.LUIS.Programmatic (Nuget)](https://www.nuget.org/packages/Cognitive.LUIS.Programmatic)
* [Projeto Open Source](https://github.com/andreluizsecco/Cognitive-LUIS-Programmatic)

Para ver mais detalhes do projeto, assista essa live feita no Canal .NET: [https://www.youtube.com/watch?v=obMTUBKxwpI&t=2h3m30s](https://www.youtube.com/watch?v=obMTUBKxwpI&t=2h3m30s)

## Getting started
* Execute o script SQL da pasta **Database** para criar a base de dados e a tabela que irá armazenar as mensagens;
* Importe o arquivo JSON da pasta **LuisApp** na sua conta no site [luis.ai](http://luis.ai);
* Abra o projeto na pasta **src** e substitua a string **{ModelId}** pelo Id que foi criado quando criou seu app no portal do LUIS. Substitua também a string **{YourSubscriptionKey}** pela chave criada na sua conta do Azure (ou a chave temporária do LUIS) que possibilita usar e modificar seu LUIS App.
