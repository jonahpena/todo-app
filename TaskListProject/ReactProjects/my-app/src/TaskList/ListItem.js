import React from "react";
import "./TaskList.css";

function ListItem({ item, index, handleCompleteItem, handleRemoveItem }) {
  return (
    <li className="list-item">
      <label>
        <input
          type="checkbox"
          onChange={() => handleCompleteItem(item, index)}
          checked={false}
        />
        <span></span>
      </label>
      <span className="list-item-text">{item.title}</span>
      <span
        className="material-icons delete-icon"
        onClick={() => handleRemoveItem(item.id)}
      >
        close
      </span>
    </li>
  );
}

export default ListItem;
