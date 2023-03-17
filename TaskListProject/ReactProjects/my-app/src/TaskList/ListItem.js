import React from "react";
import { useState, useEffect, useRef } from "react";
import "./TaskList.css";

function ListItem({
  item,
  index,
  handleCompleteItem,
  handleRemoveItem,
  handleUpdateItem,
  handleEditingTask,
  editingItemIdRef,
}) {
  const [isEditing, setIsEditing] = useState(false);
  const [editedTitle, setEditedTitle] = useState(item.title);
  const [previousIsEditing, setPreviousIsEditing] = useState(false);
  const inputRef = useRef(null);

  useEffect(() => {
    if (!isEditing && editedTitle.trim()) {
      handleUpdateItem(item.id, editedTitle);
    }
  }, [isEditing]);

  useEffect(() => {
    if (isEditing) {
      inputRef.current.focus();
    }
  }, [isEditing]);

  const handleClick = () => {
    if (
      editingItemIdRef.current === null ||
      editingItemIdRef.current !== item.id
    ) {
      setIsEditing(true);
      handleEditingTask(item.id);
      editingItemIdRef.current = item.id;
    }
  };

  useEffect(() => {
    if (
      editingItemIdRef.current !== null &&
      editingItemIdRef.current !== item.id
    ) {
      setIsEditing(false);
      setEditedTitle(item.title);
    }
  }, [editingItemIdRef.current, item.title]);

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
              ref={inputRef}
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
