// Show create button only for users with permission
document.addEventListener('DOMContentLoaded', function() {
    const createFieldBtn = document.getElementById('createFieldBtn');
    
    if (createFieldBtn && window.AuthManager) {
        // Check if user is logged in and has CREATE_FIELD permission
        if (AuthManager.isLoggedIn() && AuthManager.hasPermission(PERMISSIONS.CREATE_FIELD)) {
            createFieldBtn.style.display = 'inline-block';
        }
    }
});
