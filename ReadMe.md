# FAtiMA Toolkit

<!---[![Build status](https://ci.appveyor.com/api/projects/status/84vfpgaawun3nxqx?svg=true)](https://ci.appveyor.com/project/samuelfm/fatima-toolkit) -->
<!---[![AppVeyor tests](https://img.shields.io/appveyor/tests/samuelfm/fatima-toolkit.svg)](https://ci.appveyor.com/project/samuelfm/fatima-toolkit/build/tests) --->

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

### Latest Release: https://github.com/GAIPS-INESC-ID/FAtiMA-Toolkit/releases/tag/v4.1

### Tutorials
To get started, we recommed that you check the code examples of each asset in the project folder named *Tutorials*

Additionally users car check the oficial website's "Get Started" section to learn more: https://fatima-toolkit.eu/home/get-started/

### Training Materials

- Please check the oficial FAtiMA Website for updated training materials: https://fatima-toolkit.eu/home/get-started/



### Unity Demo Repository
The following repository contains the source code for the prototypes made in Unity to showcase the toolkit: 
https://github.com/GAIPS-INESC-ID/FAtiMA-Toolkit-UnityDemo

### FAtiMA-Toolkit Overview Video:
https://youtu.be/Qd0Re6H9V2o


### Android
We also support android: https://www.dropbox.com/s/vdebxhowao3yqcw/FAtiMA-Demo.apk?dl=0

### Documentation
For each asset, there is a pdf document describing its API in the folder named *Documentation*

### License
Apache 2.0

### Contact
For any questions, suggestions and feedback please contact Manuel Guimarães (manuel.m.guimaraes@tecnico.ulisboa.pt). 


[rage-link]: <http://rageproject.eu//>
[fatima-link]: <http://link.springer.com/chapter/10.1007%2F978-3-319-12973-0_3>
[fear-not]: <https://www.youtube.com/watch?v=x0Hzw4WG4iI>
[sueca-link]: <https://vimeo.com/153148841>
[traveller-link]: <http://www.fdg2013.org/program/workshops/papers/IDGEI2013/idgei2013_9.pdf>
