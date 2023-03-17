import React from "react";
import "./TaskList.css";

function ListItem({ item, index, handleCompleteItem, handleRemoveItem }) {
  return (
    <li className="list-item">
      <div className="list-item-inner">
        <label>
          <input
            type="checkbox"
            onChange={() => handleCompleteItem(item, index)}
            checked={false}
          />
          <span></span>
        </label>
      </div>
      <div className="list-item-text">{item.title}</div>
      <div></div>
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
