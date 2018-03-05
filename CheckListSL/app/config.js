angular.module('myApp').constant('config', {
    author: 'Andrei Surzhan',
    webApi: {
        baseUrl: '/api/v1/checklists'
    },
    translationService: {
        copyRights: {
            text: 'Powered by Yandex.Translate',
            link: 'http://translate.yandex.com/'
        },
        protocol: 'https',
        host: 'translate.yandex.net',
        resource: 'api/v1.5/tr.json',
        key: 'trnsl.1.1.20171205T160848Z.40acfb7112cde17a.f2a30cf37f048cfe8b859d205cd515aa4e775ff5',
        languages: [
            {
                language: 'English',
                code: 'en'
            },
            {
                language: 'Spanish',
                code: 'es'
            },
            {
                language: 'Russian',
                code: 'ru'
            }
        ]
    }
})