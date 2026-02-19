// Icon preview
document.getElementById('categoryIcon').addEventListener('input', function(e) {
    const icon = e.target.value || '📂';
    document.getElementById('iconPreview').textContent = icon;
});

// Auto-generate slug from category name
document.getElementById('categoryName').addEventListener('input', function(e) {
    const slugInput = document.getElementById('slug');
    if (!slugInput.value || slugInput.dataset.autoGenerate !== 'false') {
        const slug = e.target.value
            .toLowerCase()
            .replace(/ə/g, 'e')
            .replace(/ı/g, 'i')
            .replace(/ğ/g, 'g')
            .replace(/ü/g, 'u')
            .replace(/ö/g, 'o')
            .replace(/ş/g, 's')
            .replace(/ç/g, 'c')
            .replace(/[^a-z0-9]+/g, '-')
            .replace(/^-+|-+$/g, '');
        slugInput.value = slug;
        slugInput.dataset.autoGenerate = 'true';
    }
});

// Prevent auto-generation if user manually edits slug
document.getElementById('slug').addEventListener('input', function() {
    this.dataset.autoGenerate = 'false';
});

// Handle form submission
document.getElementById('createCategoryForm').addEventListener('submit', function(e) {
    e.preventDefault();
    
    // Collect form data
    const formData = new FormData(this);
    
    // Create category object
    const categoryData = {
        name: formData.get('categoryName'),
        icon: formData.get('categoryIcon'),
        description: formData.get('description'),
        status: formData.get('status'),
        sortOrder: formData.get('sortOrder'),
        showInMenu: document.getElementById('showInMenu').checked,
        featured: document.getElementById('featured').checked,
        slug: formData.get('slug'),
        metaTitle: formData.get('metaTitle'),
        metaDescription: formData.get('metaDescription')
    };
    
    console.log('Category Data:', categoryData);
    
    // Show success message
    alert('Kateqoriya uğurla yaradıldı! ✓');
    
    // Redirect to admin panel
    window.location.href = 'admin-panel.html';
});
