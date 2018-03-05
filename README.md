# CheckListSL
Checklist application that aimed to help people to memorize foreign words while doing shopping, chores etc.

## Features
Application dymanically defines inputted language and translates into other ones

![landingPageView](/landingPageView.png)

## Plans for futures

### Refactoring

#### Global
- move back-end and front-end into separate repositories
- rewrite back-end into nodejs or django to be able to host it on free hostings
- DB Migration process
- build project with gulp and bower for front-end
#### Other
- angular validation on inputs
- make angularjs components and custom directives (checklist, item)
- Better error handling both sides
- logging (INFO, DEBUG, ERORR, WARN) in back-end and front-end
- Solve the problem about having auth credantials in angular
- Refactoring of *.less
	- prefixes
- add unittests and web api tests
- consider looking into bower, npm and gulp

### Features
- Registration/Loging/Auth
- Favorites in checklists and items
- language settings/Settings page (switch between languages, possibility to add languages)
- Responsive views - mobile friendly. Change the layout to :
	
  ```
  Checklist1(Active)>
	   Item1
	   Item2
	CheckList2(Inactive)<
  ```
  
- change order of the checklists and items via D&D
- move call for External Translation service to the BE and use Google API

## Installation
Comming up

## Tests
Commint up

