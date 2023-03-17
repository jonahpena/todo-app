import React from "react";
import "./TaskList.css";

function ListItem({ item, index, handleCompleteItem, handleRemoveItem }) {
  return (
    <li>
      <div className="list-item">
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
        <div
          className="material-icons incomplete-delete-icon"
          onClick={() => handleRemoveItem(item.id)}
        >
          close
        </div>{" "}
      </div>

      <div></div>
    </li>
  );
}

export default ListItem;
