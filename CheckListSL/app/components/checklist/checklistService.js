(() => {
    angular.module('myApp').factory('checklistFactory', ['config', '$http', '$q', (config, $http) => {
        const urlBase = config.webApi.baseUrl;
        let checklists = [];

        return {
            insertChecklist(newChecklist) {
                return $http.post(urlBase, newChecklist).then((response) => {
                    return response.data;
                }, (error) => {
                    console.error(error);
                });
            },

            getChecklists() {
                return $http.get(urlBase).then((response) => {
                    return response.data;
                }, (error) => {
                    console.error(error);
                });
            },

            getChecklistById(id) {
                return $http.get(`${urlBase}/${id}`).then((response) => {
                    return response.data;
                }, (error) => {
                    console.error(error);
                });
            },

            updateChecklist(id, checklist) {
                return $http.patch(`${urlBase}/${id}`, checklist).then((response) => {
                    return response.data;
                }, (error) => {
                    console.error(error);
                });
            },

            deleteChecklist(id) {
                return $http.delete(`${urlBase}/${id}`).then((response) => {
                    return response.data;
                }, (error) => {
                    console.error(error);
                });
            },

            getAllChecklists() {
                return checklists;
            },

            getActiveChecklist() {
                return checklists.find((checklist) => {
                    if (checklist.isActive === true) {
                        return checklist
                    }
                });
            },

            setAllChecklists(newChecklists) {
                checklists = newChecklists;
            }
        }
    }]);
})();