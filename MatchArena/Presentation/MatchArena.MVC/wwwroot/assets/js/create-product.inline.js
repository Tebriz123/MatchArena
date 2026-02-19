// Handle form submission
document.getElementById('createProductForm').addEventListener('submit', function(e) {
    e.preventDefault();
    
    // Collect form data
    const formData = new FormData(this);
    
    // Collect selected colors
    const colors = [];
    document.querySelectorAll('input[name="colors"]:checked').forEach(checkbox => {
        colors.push(checkbox.value);
    });
    
    // Collect selected sizes
    const sizes = [];
    document.querySelectorAll('input[name="sizes"]:checked').forEach(checkbox => {
        sizes.push(checkbox.value);
    });
    
    // Validate colors and sizes
    if (colors.length === 0) {
        alert('⚠️ Ən azı bir rəng seçməlisiniz!');
        document.getElementById('colorsGroup').scrollIntoView({ behavior: 'smooth', block: 'center' });
        return;
    }
    
    if (sizes.length === 0) {
        alert('⚠️ Ən azı bir ölçü seçməlisiniz!');
        document.getElementById('sizesGroup').scrollIntoView({ behavior: 'smooth', block: 'center' });
        return;
    }
    
    // Create product object (in real implementation, this would be sent to API)
    const productData = {
        name: formData.get('productName'),
        category: formData.get('category'),
        brand: formData.get('brand'),
        description: formData.get('description'),
        price: formData.get('price'),
        comparePrice: formData.get('comparePrice'),
        stock: formData.get('stock'),
        sku: formData.get('sku'),
        status: formData.get('status'),
        colors: colors,
        sizes: sizes,
        featured: document.getElementById('featured').checked,
        bestseller: document.getElementById('bestseller').checked,
        newArrival: document.getElementById('newArrival').checked,
        onSale: document.getElementById('onSale').checked,
        weight: formData.get('weight'),
        material: formData.get('material'),
        metaTitle: formData.get('metaTitle'),
        metaDescription: formData.get('metaDescription'),
        tags: formData.get('tags')
    };
    
    console.log('Product Data:', productData);
    
    // Show success message
    alert('Məhsul uğurla yaradıldı! ✓');
    
    // Redirect to admin panel
    window.location.href = 'admin-panel.html';
});
