import React from 'react';
import { deleteProduct } from './api'; 

function ProductDelete({ productId }) {
  const handleDelete = () => {
    deleteProduct(productId).then(() => {
      // Обработка успешного удаления товара
    });
  };

  return (
    <div>
      <h2>Delete Product</h2>
      <button onClick={handleDelete}>Delete</button>
    </div>
  );
}

export default ProductDelete;
