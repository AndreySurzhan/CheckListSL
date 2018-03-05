(() => {
    angular.module('myApp').factory('itemFactory', ['checklistFactory', 'config', '$http', (checklistFactory, config, $http) => {
        const urlBase = config.webApi.baseUrl
        let items = [];

        return {
            insertItem(checklistId, newItem) {
                return $http.post(`${urlBase}/${checklistId}/items`, newItem).then((response) => {
                    return response.data;
                }, (error) => {
                    console.error(error);
                });
            },

            getItems(checklistId) {
                return $http.get(`${urlBase}/${checklistId}/items`).then((response) => {
                    return response.data;
                }, (error) => {
                    console.error(error);
                });
            },

            updateItem(checklistId, itemId, item) {
                return $http.patch(`${urlBase}/${checklistId}/items/${itemId}`, item).then((response) => {
                    return response.data;
                }, (error) => {
                    console.error(error);
                });
            },

            deleteItem(checklistId, itemId) {
                return $http.delete(`${urlBase}/${checklistId}/items/${itemId}`).then((response) => {
                    return response.data;
                }, (error) => {
                    console.error(error);
                });
            }
        }
    }]);
})();
