import React, { useState } from "react";
import { Popup, Form, Button } from "devextreme-react";
import { GroupItem, SimpleItem } from "devextreme-react/cjs/form";
import "./index.scss";
import ApiRoutes from "../../api";
import Book from "../../types/Book";

const positionOptions = {
  items: ["Digital", "Physical"],
  value: "",
};

export default function AddBookModal({
  onClose,
  handleAddCallback,
}: {
  onClose: () => void;
  handleAddCallback: () => Promise<void>;
}) {
  const [formData, setFormData] = useState<Book>({
    title: "",
    totalCopies: 0,
    copiesInUse: 0,
    type: "",
    category: "",
    Author: "",
  });

  const [validationErrors, setValidationErrors] = useState<string[]>([]);

  const handleFieldChange = (e: any) => {
    if (e.dataField === "totalCopies" || e.dataField === "copiesInUse") {
      const value = parseInt(e.value || "0", 10);
      setFormData({ ...formData, [e.dataField]: Math.max(0, value) });
    } else {
      setFormData({ ...formData, [e.dataField]: e.value });
    }
  };

  const validateForm = (): boolean => {
    const errors: string[] = [];

    if (!formData.title) {
      errors.push("Title is required");
    } else if (formData.title.length < 5) {
      errors.push("Title must be at least 5 characters long");
    }

    if (!formData.Author) {
      errors.push("Author is required");
    } else if (formData.Author.length < 5) {
      errors.push("Author must be at least 5 characters long");
    }

    if (!formData.category) {
      errors.push("Category is required");
    }

    if (!formData.type) {
      errors.push("Type is required");
    }

    setValidationErrors(errors);

    return errors.length === 0;
  };

  const handleSubmit = async () => {
    if (validateForm()) {
      const newBook: Book = {
        category: formData.category,
        copiesInUse: formData.copiesInUse,
        title: formData.title,
        totalCopies: formData.totalCopies,
        type: formData.type,
        Author: formData.Author,
      };
      await ApiRoutes.addBook(newBook);

      await handleAddCallback();

      onClose();
    }
  };

  return (
    <Popup
      visible={true}
      onHiding={onClose}
      showTitle={true}
      title="Add Book"
      width={600}
      height={"auto"}
      dragEnabled={false}
      closeOnOutsideClick={false}
    >
      <Form formData={formData} onFieldDataChanged={handleFieldChange}>
        <GroupItem cssClass="form-group" colCount={1}>
          <SimpleItem dataField="title" />
        </GroupItem>
        <GroupItem cssClass="form-group" colCount={2}>
          <SimpleItem dataField="Author" />
          <SimpleItem dataField="category" editorType="dxTextBox" />
        </GroupItem>
        <GroupItem cssClass="form-group" colCount={2}>
          <SimpleItem
            dataField="totalCopies"
            editorType="dxNumberBox"
            editorOptions={{
              width: "100%",
              showSpinButtons: true,
            }}
          />
          <SimpleItem
            dataField="copiesInUse"
            editorType="dxNumberBox"
            editorOptions={{
              width: "100%",
              showSpinButtons: true,
            }}
          />
        </GroupItem>
        <GroupItem cssClass="form-group" colCount={2}>
          <SimpleItem
            dataField="type"
            editorType="dxSelectBox"
            editorOptions={positionOptions}
          />
        </GroupItem>
      </Form>
      {validationErrors.length > 0 && (
        <div className="validation-errors">
          {validationErrors.map((error, index) => (
            <div key={index} className="error">
              <span className="error-icon">&#10006;</span>
              <span className="error-message">{error}</span>
            </div>
          ))}
        </div>
      )}
      <div className="dx-popup-action-panel">
        <Button onClick={handleSubmit} text="Add" />
        <Button onClick={onClose} text="Cancel" />
      </div>
    </Popup>
  );
}
