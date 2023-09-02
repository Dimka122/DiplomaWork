import React, { useState, useEffect } from 'react';
import { getCategories, addOrUpdateProduct } from './api'; // Ваши модули для общения с API

function ProductForm() {
  const [product, setProduct] = useState({ id: 0, name: '', categoryId: 0, retailPrice: 0 });
  const [categories, setCategories] = useState([]);

  useEffect(() => {
    getCategories().then(data => setCategories(data));
  }, []);

  const handleInputChange = event => {
    const { name, value } = event.target;
    setProduct(prevProduct => ({ ...prevProduct, [name]: value }));
  };

  const handleSubmit = event => {
    event.preventDefault();
    addOrUpdateProduct(product).then(data => {
      // Обработка успешного добавления/обновления товара
    });
  };

  return (
    <div>
      <h2>Add/Edit Product</h2>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          name="name"
          value={product.name}
          onChange={handleInputChange}
          placeholder="Product Name"
        />
        <select name="categoryId" value={product.categoryId} onChange={handleInputChange}>
          <option value={0}>Select Category</option>
          {categories.map(category => (
            <option key={category.id} value={category.id}>
              {category.name}
            </option>
          ))}
        </select>
        <input
          type="number"
          name="retailPrice"
          value={product.retailPrice}
          onChange={handleInputChange}
          placeholder="Retail Price"
        />
        <button type="submit">Save</button>

      </form>
    </div>
  );
}

export default ProductForm;
