// Load product data when page loads
document.addEventListener('DOMContentLoaded', function() {
    // Get product ID from URL parameter
    const urlParams = new URLSearchParams(window.location.search);
    const productId = urlParams.get('id');
    
    if (productId) {
        // Load product data (this would normally fetch from API)
        loadProductData(productId);
    }
});

// Handle form submission
document.getElementById('updateProductForm').addEventListener('submit', function(e) {
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
        id: document.getElementById('productId').value,
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
    
    console.log('Updated Product Data:', productData);
    
    // Show success message
    alert('Məhsul uğurla yeniləndi! ✓');
    
    // Redirect to admin panel
    window.location.href = 'admin-panel.html';
});

function loadProductData(productId) {
    // This is a placeholder - in real implementation, this would fetch from API
    // For now, we'll use demo data
    const demoProducts = {
        'P001': {
            name: 'Futbol Topu - Pro',
            category: 'C001',
            brand: 'Adidas',
            description: 'Professional quality football for competitive matches',
            price: 45.00,
            comparePrice: 55.00,
            stock: 120,
            sku: 'FB-PRO-001',
            status: 'active',
            colors: ['CL001', 'CL002'],
            sizes: ['S008'],
            featured: true,
            bestseller: false,
            newArrival: false,
            onSale: true,
            weight: 450,
            material: 'Dəri',
            metaTitle: 'Professional Football - Adidas Pro',
            metaDescription: 'High quality football for professional matches',
            tags: 'futbol, top, professional',
            image: 'assets/img/products/football-ball.jpg'
        },
        'P002': {
            name: 'Futbol Ayaqqabısı - Nike',
            category: 'C002',
            brand: 'Nike',
            description: 'Professional Nike football shoes for players',
            price: 120.00,
            comparePrice: 0,
            stock: 45,
            sku: 'FS-NIKE-002',
            status: 'active',
            colors: ['CL001', 'CL002', 'CL003'],
            sizes: ['S006', 'S007'],
            featured: false,
            bestseller: true,
            newArrival: false,
            onSale: false,
            weight: 280,
            material: 'Süni dəri, Mesh',
            metaTitle: 'Nike Football Shoes',
            metaDescription: 'High performance football shoes',
            tags: 'futbol, ayaqqabı, nike',
            image: 'assets/img/products/football-shoes.jpg'
        },
        'P003': {
            name: 'Baldır Qoruyucusu',
            category: 'C003',
            brand: 'Pro Guard',
            description: 'Professional shin guards for maximum protection',
            price: 25.00,
            comparePrice: 0,
            stock: 5,
            sku: 'SG-PRO-003',
            status: 'low-stock',
            colors: ['CL001', 'CL002'],
            sizes: ['S003', 'S004'],
            featured: false,
            bestseller: false,
            newArrival: true,
            onSale: false,
            weight: 120,
            material: 'Plastik, Köpük',
            metaTitle: 'Professional Shin Guards',
            metaDescription: 'Maximum protection for players',
            tags: 'baldır, qoruyucu, təhlükəsizlik',
            image: 'assets/img/products/shin-guards.jpg'
        }
    };

    const product = demoProducts[productId] || demoProducts['P001'];
    
    // Fill form fields
    document.getElementById('productId').value = productId;
    document.getElementById('productName').value = product.name;
    document.getElementById('category').value = product.category;
    document.getElementById('brand').value = product.brand;
    document.getElementById('description').value = product.description;
    document.getElementById('price').value = product.price;
    if (product.comparePrice) document.getElementById('comparePrice').value = product.comparePrice;
    document.getElementById('stock').value = product.stock;
    document.getElementById('sku').value = product.sku;
    document.getElementById('status').value = product.status;
    
    // Show current image
    if (product.image) {
        document.getElementById('currentProductImage').src = product.image;
        document.getElementById('currentProductImage').style.display = 'block';
    }
    
    // Set checkboxes
    product.colors.forEach(color => {
        const checkbox = document.querySelector(`input[name="colors"][value="${color}"]`);
        if (checkbox) checkbox.checked = true;
    });
    
    product.sizes.forEach(size => {
        const checkbox = document.querySelector(`input[name="sizes"][value="${size}"]`);
        if (checkbox) checkbox.checked = true;
    });
    
    document.getElementById('featured').checked = product.featured;
    document.getElementById('bestseller').checked = product.bestseller;
    document.getElementById('newArrival').checked = product.newArrival;
    document.getElementById('onSale').checked = product.onSale;
    
    if (product.weight) document.getElementById('weight').value = product.weight;
    if (product.material) document.getElementById('material').value = product.material;
    if (product.metaTitle) document.getElementById('metaTitle').value = product.metaTitle;
    if (product.metaDescription) document.getElementById('metaDescription').value = product.metaDescription;
    if (product.tags) document.getElementById('tags').value = product.tags;
}
