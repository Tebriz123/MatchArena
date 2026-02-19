// Show create button only for users with permission
document.addEventListener('DOMContentLoaded', function() {
    const createTeamBtn = document.getElementById('createTeamBtn');
    
    if (createTeamBtn && window.AuthManager) {
        // Check if user is logged in and has CREATE_TEAM permission
        if (AuthManager.isLoggedIn() && AuthManager.hasPermission(PERMISSIONS.CREATE_TEAM)) {
            createTeamBtn.style.display = 'inline-block';
        }
    }
});
