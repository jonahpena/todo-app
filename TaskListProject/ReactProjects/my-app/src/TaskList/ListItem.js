import React from "react";
import { useState, useEffect } from "react";
import "./TaskList.css";

function ListItem({
  item,
  index,
  handleCompleteItem,
  handleRemoveItem,
  handleUpdateItem,
}) {
  const [isEditing, setIsEditing] = useState(false);
  const [editedTitle, setEditedTitle] = useState(item.title);
  useEffect(() => {
    if (!isEditing && editedTitle.trim()) {
      handleUpdateItem(item.id, editedTitle);
    }
  }, [isEditing]);

  const handleClick = () => {
    setIsEditing(true);
  };

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
        <div className="list-item-text" onClick={handleClick}>
          {isEditing ? (
            <input
              type="text"
              value={editedTitle}
              onChange={(e) => setEditedTitle(e.target.value)}
              onBlur={() => setIsEditing(false)}
              onKeyPress={(e) => {
                if (e.key === "Enter") {
                  setIsEditing(false);
                }
              }}
            />
          ) : (
            item.title
          )}
        </div>
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
