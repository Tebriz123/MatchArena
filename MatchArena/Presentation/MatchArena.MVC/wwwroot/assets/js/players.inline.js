// Show create button only for logged in users (any role can create player profile)
document.addEventListener('DOMContentLoaded', function() {
    const createPlayerBtn = document.getElementById('createPlayerBtn');
    
    if (createPlayerBtn && window.AuthManager) {
        // Any logged in user can create a player profile
        if (AuthManager.isLoggedIn()) {
            createPlayerBtn.style.display = 'inline-block';
        }
    }
});
