(() => {
    angular.module('myApp').factory('translationFactory', ['config', '$http', '$q', (config, $http, $q) => {
        //translate to the list of the languages
        //1. Detect 2. Translate
        const service = config.translationService
        let urlBase = `${service.protocol}://${service.host}/${service.resource}`

        function getLanguageName(languages, code) {
            return languages.filter(l => l.code === code)[0].language
        }

        return {
            //translate to the list of the languages
            //1. Detect 2. Translate
            getTranslations(text) {
                let deferred = $q.defer();
                let languages = service.languages.map(lang => lang.code)

                text = text.toString();

                console.log("GET LANG CODES: ", languages);
                console.log("TEXT TO TRANSLATE: ", text);

                $http.post(`${urlBase}/detect?hint=${languages.join()}&key=${service.key}`, `text=${text}`, {
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
                    }
                })
                    .then((response) => {
                        if (response.data.code !== 200) {
                            console.error('Translation service detect Api responded with status code: ', response.data.code);

                            return deferred.reject();
                        }

                        if (!response.data.lang) {
                            console.error('Detected language is not recived: ');

                            return deferred.reject();
                        }

                        console.log("DETECTED LANG: ", response.data.lang);

                        return response.data.lang;
                    }, (error) => {
                        console.error(error);
                        deferred.reject(error);
                    })
                    .then((langFrom) => {
                        let langs = service.languages
                        let promises = [];

                        langs = langs.filter(l => l.code !== langFrom);
                        langs = langs.map(l => l.code);

                        console.log("LANGS TO TRANSLATE:", langs);

                        langs.map((langTo) => {
                            let promise = $http.post(`${urlBase}/translate?lang=${langFrom}-${langTo}&key=${service.key}`, `text=${text}`, {
                                headers: {
                                    'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8'
                                }
                            }).then((response) => {
                                let translation = {};

                                if (response.data.code !== 200) {
                                    console.error('Translation service Translation Api responded with status code: ', response.data.code);

                                    return reject();
                                }

                                console.log(`TRANSLATION IN '${response.data.lang}': `, response.data.text[0]);

                                translation.language = service.languages.filter(l => l.code === response.data.lang.split("-").pop())[0].language;
                                translation.translationString = response.data.text[0];

                                return translation;
                            }, (error) => {
                                console.error(error);
                                deferred.reject(error);
                            })

                            promises.push(promise);
                        });

                        return $q.all(promises);
                    })
                    .then((translations) => {
                        deferred.resolve(translations);
                    }, (error) => {
                        console.error(error);
                        deferred.reject(error);
                    });

                return deferred.promise;
            }
        };
    }])
})();