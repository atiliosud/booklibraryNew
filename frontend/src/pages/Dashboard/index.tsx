import "devextreme/dist/css/dx.light.css";
import React, { useState, ChangeEvent, useEffect } from "react";
import {
  DataGrid,
  Column,
  Pager,
  Paging,
  Scrolling,
  GroupPanel,
  SearchPanel,
  Grouping,
  ColumnChooser,
  Sorting,
  HeaderFilter,
} from "devextreme-react/data-grid";
import { Autocomplete, Button, SelectBox } from "devextreme-react";
import "./index.scss";
import ODataStore from "devextreme/data/odata/store";
import { createStore } from "devextreme-aspnet-data-nojquery";
import ApiRoutes from "../../api";
import Book from "../../types/Book";
import "./index.scss";
import AddBookModal from "../../components/AddBookModal";

export interface IBook {
  bookId: string;
  title: string;
  firstName: string;
  lastName: string;
  birthDate: Date;
  totalCopies: number;
  copiesInUse: number;
  type: string;
  isbn: string;
  category: string;
}

const Dashboard: React.FC = () => {
  const [books, setBooks] = useState<IBook[]>([]);
  const [showModal, setShowModal] = useState<boolean>(false);

  const loadData = async () => {
    const { data } = await ApiRoutes.getBooks();
    setBooks(data);
  };

  useEffect(() => {
    loadData();
  }, []);

  return (
    <div className="books-page">
      {showModal && (
        <AddBookModal
          onClose={async () => {
            setShowModal(false);
          }}
          handleAddCallback={loadData}
        />
      )}
      <div
        style={{
          display: "flex",
          alignItems: "center",
          justifyContent: "space-between",
          marginBottom: 20,
        }}
      >
        <h1 className="books-page__title">Books</h1>
        <Button
          onClick={() => {
            setShowModal(true);
          }}
          icon="add"
          className="books-page__search-button"
        ></Button>
      </div>

      <DataGrid
        id="data-grid"
        showBorders={true}
        remoteOperations={true}
        dataSource={books}
      >
        <Sorting mode="multiple" />
        <GroupPanel visible={true} />
        <HeaderFilter visible={true} />
        <SearchPanel width={400} visible={true} highlightCaseSensitive={true} />
        <Grouping autoExpandAll={false} />
        <Column dataField="bookId" caption="ID" dataType="string" />
        <Column dataField="title" caption="Title" dataType="string" />
        <Column dataField="author" caption="Author" dataType="string" />
        <Column
          dataField="totalCopies"
          caption="Total Copies"
          dataType="number"
        />
        <Column
          dataField="copiesInUse"
          caption="Copies in Use"
          dataType="number"
        />
        <Column dataField="type" caption="Type" dataType="string" />
        <Column dataField="category" caption="Category" dataType="string" />
      </DataGrid>
    </div>
  );
};

export default Dashboard;
