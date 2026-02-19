document.addEventListener('DOMContentLoaded', () => {
    const urlParams = new URLSearchParams(window.location.search);
    const paymentId = urlParams.get('id');
    
    if (!paymentId) {
        window.location.href = 'index.html';
        return;
    }
    
    displayPaymentDetails(paymentId);
});

function displayPaymentDetails(paymentId) {
    const detailsDiv = document.getElementById('paymentDetails');
    
    // In production, fetch from backend
    // For now, display basic info
    detailsDiv.innerHTML = `
        <div class="detail-row">
            <span class="detail-label">Ödəniş ID:</span>
            <span class="detail-value">${paymentId}</span>
        </div>
        <div class="detail-row">
            <span class="detail-label">Tarix:</span>
            <span class="detail-value">${new Date().toLocaleString('az-AZ')}</span>
        </div>
        <div class="detail-row">
            <span class="detail-label">Status:</span>
            <span class="detail-value success-badge">Uğurlu</span>
        </div>
    `;
}
