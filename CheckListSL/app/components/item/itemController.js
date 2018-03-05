(() => {
    angular.module('myApp').controller('itemController', ['$scope', 'config', 'translationFactory', 'checklistFactory', 'itemFactory', ($scope, config, translationFactory, checklistFactory, itemFactory) => {
        let vm = this;
        vm.translationServiceCopyRights = config.translationService.copyRights;
        vm.newItem = {};

        $scope.$watch(() => {
            return checklistFactory.getActiveChecklist();
        }, (newVal, oldVal, scope) => {
            vm.activeChecklist = checklistFactory.getActiveChecklist();

            console.log('GET ATIVE CHECKLIST: ', vm.activeChecklist);

            if (vm.activeChecklist && vm.activeChecklist.hasOwnProperty("items")) {
                if (!(vm.activeChecklist.items instanceof Array)) {
                    vm.activeChecklist.items = [];
                }

                console.log('GET ITEMS: ', vm.activeChecklist.items)
            }
        }, true)

        vm.addItem = () => {
            translationFactory.getTranslations(vm.newItem.name).then((translations) => {
                newItem.translations = translations;

                console.log('GET TRANSLATIONS WHILE ADDING ITEM: ', newItem.translations);

                return itemFactory.insertItem(vm.activeChecklist.id, newItem).then((addedItem) => {
                    vm.activeChecklist.items.push(addedItem);
                    vm.newItem = {};
                });
            })
        };

        vm.updateItem = (item) => {
            item.isEditing = false;
            item.translations = [];

            translationFactory.getTranslations(item.name).then((translations) => {
                item.translations = translations;

                console.log('GET TRANSLATIONS WHILE EDITING ITEM: ', item.translations);

                return itemFactory.updateItem(vm.activeChecklist.id, item.id, item);
            })
        };

        vm.checkItem = (item) => {
            if (item.isChecked) {
                item.isChecked = false;
            } else {
                item.isChecked = true;
            }

            return itemFactory.updateItem(vm.activeChecklist.id, item.id, item);
        }

        vm.deleteItem = (item) => {
            const index = vm.activeChecklist.items.indexOf(item);

            vm.activeChecklist.items.splice(index, 1);

            return itemFactory.deleteItem(vm.activeChecklist.id, item.id);
        };

        vm.editItem = (item) => {
            item.isEditing = true;
        };

        vm.cancelEdit = (item) => {
            item.isEditing = false;
        };

        return vm;
    }]);
})();