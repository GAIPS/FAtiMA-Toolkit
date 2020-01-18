# FAtiMA Toolkit

[![Build status](https://ci.appveyor.com/api/projects/status/84vfpgaawun3nxqx?svg=true)](https://ci.appveyor.com/project/samuelfm/fatima-toolkit)
[![AppVeyor tests](https://img.shields.io/appveyor/tests/samuelfm/fatima-toolkit.svg)](https://ci.appveyor.com/project/samuelfm/fatima-toolkit/build/tests)

### Code Coverage (76%)

### Oficial Website: https://fatima-toolkit.eu/

### Description

FAtiMA Toolkit is a collection of tools/assets designed for the creation of characters with social and emotional intelligence. Currently, the project is actively being developed in the context of the EU-funded project named [RAGE][rage-link]. As implied by its name, the toolkit is a continuation of the work done in developing the [FAtiMA][fatima-link] agent architecture. This architecture was initially released in 2005 and, since then, it has been used to control the behaviour of virtual characters and social robots in several research applications such as:  

- [Traveller][traveller-link] - A serious game for raising intercultural awareness;
- [SUECA][sueca-link]  - A social robot that plays an augmented reality card game;
- [FearNot!][fear-not] - A serious game for teaching children how to cope with bullying;

Besides porting the code from Java to C#, the toolkit offers many significant improvements over the previous form of FAtiMA. These improvements were guided by the following goals:

- Modular design;
- Simple integration with current game engines and other AI frameworks;
- Accessible authoring tools and proper documentation;
- Able to work on multiple application environments (Windows, Mac, Browser, iOS, Android)

With these goals in mind, the toolkit is composed of several assets. Each has a single core purpose, reflected in its external API that provides an abstraction for the asset's internal mechanisms. All assets can be used directly in any game engine that is able to import C# standard libraries, such as Unity. Each asset also contains an authoring tool with a GUI to aids its configuration. Currently, the following assets are included in the toolkit:

- Emotional Appraisal - Manages the beliefs and the emotional state of the character according to how it is configured to judge the events that happen in the game world;

- Emotional Decision Making - Decides how the character acts taking into account its emotions and beliefs about the state of the world;

- Social Importance Dynamics - Adds the ability for the character to judge if an action is socially appropriate or not depending on how it perceives others from a relational standpoint.

- Role Play Character - Integrates a combination of the previous three assets in a simplified perception-action cycle.

- Integrated Authoring Tool - Manages the authoring of a scenario including its characters and respective dialogues.

- Real Time Emotion Recognition - Able to infer the player's emotional state by combining multiple sources of affective inputs.

### Latest Release: https://github.com/GAIPS-INESC-ID/FAtiMA-Toolkit/releases/tag/v4.0

### Tutorials
To get started, we recommed that you check the code examples of each asset in the project folder named *Tutorials*

Additionally users car check the oficial website's "Get Started" section to learn more: https://fatima-toolkit.eu/home/get-started/

### Training Materials

- FAtiMA Website: https://fatima-toolkit.eu/home/get-started/
- GALA 2017 Tutorial Presentation: https://www.dropbox.com/s/k7ymaqwr6wftytr/FAtiMA-GALA-Tutorial.pdf?dl=0
- IAJ Lecture 1: https://www.dropbox.com/s/w6fhgtjh3jlnhwn/IAJ-FAtiMA-Part1.pdf?dl=0
- IAJ Lecture 2: https://www.dropbox.com/s/y6ldivwlqunpl3g/IAJ-FAtiMA-Part2.pdf?dl=0

### Unity Starter Kit
To start using a compiled version of the toolkit in your Unity game, download the following zip and read the instructions therein:
https://www.dropbox.com/s/kl06lbanwkbwrqx/UnityStarterKit.zip?dl=0

### Executable Demos
The following demos can be executed without Unity and they can be used to test different scenarios created with the Integrated Authoring Tool.

Note: These have been recently updated to correct a bug that occurred when the strongest emotion for one of the characcters corresponded to Shame/Pride/Gratitude

(*****NEW*****) The demo with two 3D characters had a severe issue that prevented Dialogue States from working properly. This problem has now been fixed.

- One 3D Character (Windows)  https://www.dropbox.com/s/b8tufrab8vq5f7x/FAtiMA%20One%20Character%20Demo%28Win%29.zip?dl=0
- Two 3D Characters (Windows) https://www.dropbox.com/s/px94m3qw3wf8eyk/FAtiMA%20Two%20Characters%20Demo%20%28Win%29%20Fixed.zip?dl=0

- One 3D Character (MAC OS) https://www.dropbox.com/s/z223tcz7e93ii4u/FAtiMA%20One%20Character%20Demo%20%28MAC%29.app.zip?dl=0

### Unity Demo Repository
The following repository contains the source code for the prototypes made in Unity to showcase the toolkit: 
https://github.com/GAIPS-INESC-ID/FAtiMA-Toolkit-UnityDemo

### FAtiMA-Toolkit Integrated Authoring Tool Video Demonstration:
https://www.youtube.com/watch?v=6-oLZ_DJW2U

### FAtiMA-Toolkit Role Play Character Tool Video Demonstration:
https://www.youtube.com/watch?v=2WgKs3-iI7g

### Integrated Authoring Tool

The Integrated Authoring tool is a Windows application that is used to create and edit game scenarios with the toolkit. 
The latest version now includes a world model that allows the author to define the effects of actions. These effects will then be used by the chat simulator so the author can fully test a conversational scenario between a player and one or more characters. Additionally, there is now a graph tool in the dialogue editor that automatically creates a graph structure of the existing dialogues.

- Version 2.7: https://www.dropbox.com/s/e0w7ygpd1i9rp06/AuthoringTools-v2.7.zip?dl=0

- Version 2.5: https://drive.google.com/open?id=1sfjzTDyUPM99CW6WWkXsWRNEXEllJziI

- Version 2.1: https://www.dropbox.com/s/2h5jsok824jpzow/AuthoringTools-2018-Feb-2.1.zip?dl=0

- Version 1.7: https://www.dropbox.com/s/gh0m837pyzbc786/AuthoringTools-2017-Dec.zip?dl=0-

- Version 1.4: https://www.dropbox.com/s/wuxrdlxntxnj18h/AuthoringTools-2017-Feb-10.zip?dl=0

- Version 1.3: https://www.dropbox.com/s/1a0w73vrnyl2f7a/AuthoringTools-2016-Dec-30.zip?dl=0

### Android
We also support android: https://www.dropbox.com/s/vdebxhowao3yqcw/FAtiMA-Demo.apk?dl=0

### Documentation
For each asset, there is a pdf document describing its API in the folder named *Documentation*

### License
Apache 2.0

### Contact
For any questions, suggestions and feedback please contact Samuel Mascarenhas (samuel.mascarenhas@gaips.inesc-id.pt). 


[rage-link]: <http://rageproject.eu//>
[fatima-link]: <http://link.springer.com/chapter/10.1007%2F978-3-319-12973-0_3>
[fear-not]: <https://www.youtube.com/watch?v=x0Hzw4WG4iI>
[sueca-link]: <https://vimeo.com/153148841>
[traveller-link]: <http://ecute.eu/traveller/>
