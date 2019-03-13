(function () {
    window.config = {
        instance: 'https://login.microsoftonline.com/',
        tenant: '78917ee5-ee4d-45f0-bc3b-ffe8d25ccef5',
        clientId: '06d53bdd-ace8-4fbc-8904-76c8ef4acdec',
        postLogoutRedirectUri: window.location.origin,
        cacheLocation: 'localStorage'
    };

    var authContext = new AuthenticationContext(config);

    var isCallback = authContext.isCallback(window.location.hash);
    authContext.handleWindowCallback();

    if (isCallback && !authContext.getLoginError()) {
        window.location = authContext._getItem(authContext.CONSTANTS.STORAGE.LOGIN_REQUEST);
    }
    
    window.logIn = async () => {
        var user = authContext.getCachedUser();
        if (!user) {
            authContext.login();
        }

        return true;
    }

    window.getToken = async () => {
        var tokenString;
        await authContext.acquireToken(authContext.config.clientId, async function (error, token) {
            if (error) {
                tokenString = null;
            }
            else {
                tokenString = token;
            }
        });

        return tokenString;
    }

    window.logOut = async () => {
        await authContext.logOut();

        return true;
    }

    window.getUser = async () => {
        var user = authContext.getCachedUser();

        return user;
    }
}());