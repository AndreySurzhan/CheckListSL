(() => {
    angular.module('myApp').controller('checklistController', ['$scope', 'checklistFactory', ($scope, checklistFactory) => {
        let vm = this;

        vm.newChecklist = {};
        vm.checklistsExist = true;

        checklistFactory.getChecklists().then((checklists) => {
            checklistFactory.setAllChecklists(checklists);
            vm.checklists = checklistFactory.getAllChecklists();
        });

        $scope.$watch(() => {
            return vm.checklists
        }, (newVal, oldVal, scope) => {
            vm.checklistsExist = checklistFactory.getAllChecklists().length > 0
            console.log('GET CHECKLISTS: ', checklistFactory.getAllChecklists())
        }, true)

        vm.addChecklist = () => {
            const currentlyActive = checklistFactory.getActiveChecklist();

            if (currentlyActive) {
                return setInactive(currentlyActive)
                    .then(() => {
                        vm.newChecklist.isActive = true;

                        return checklistFactory.insertChecklist(newChecklist);
                    })
                    .then((newlyAddedChecklist) => {
                        vm.newChecklist = {};
                        vm.checklists.push(newlyAddedChecklist);
                    })
            }

            vm.newChecklist.isActive = true;

            return checklistFactory.insertChecklist(newChecklist).then((newlyAddedChecklist) => {
                vm.newChecklist = {};
                vm.checklists.push(newlyAddedChecklist);
            });
        };

        vm.updateChecklist = (checklist) => {
            return checklistFactory.updateChecklist(checklist.id, checklist).then(() => {
                checklist.isEditing = false;
            });
        };

        vm.deleteChecklist = (checklist) => {
            const index = vm.checklists.indexOf(checklist);

            vm.checklists.splice(index, 1);

            return checklistFactory.deleteChecklist(checklist.id).then(() => {
                if (vm.checklists.length !== 0 && checklist.isActive) {
                    let lastChecklist = vm.checklists[vm.checklists.length - 1];

                    lastChecklist.isActive = true;

                    return checklistFactory.updateChecklist(lastChecklist.id, lastChecklist);
                }
            });
        };

        vm.editChecklist = (checklist) => {
            checklist.isEditing = true;
        };

        vm.cancelEdit = (checklist) => {
            checklist.isEditing = false;
        };

        vm.activateChecklist = (checklist) => {
            if (checklist.isActive) {
                return;
            }

            const activeChecklist = checklistFactory.getActiveChecklist();

            if (activeChecklist) {
                checklist.isActive = true;

                setInactive(activeChecklist).then((inactiveChecklist) => {
                    return checklistFactory.updateChecklist(checklist.id, checklist);
                });
            }
        }

        // PRIVATE functions
        function setInactive(activeChecklist) {
            activeChecklist.isActive = false;

            return checklistFactory.updateChecklist(activeChecklist.id, activeChecklist);
        }

        return vm;
    }]);
})();