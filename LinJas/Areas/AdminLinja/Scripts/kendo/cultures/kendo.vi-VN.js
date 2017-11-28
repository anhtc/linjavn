/*
* Kendo UI Localization Project for v2013.3.1119
* Copyright 2013 Telerik AD. All rights reserved.
*
* English GB (en-GB) Language Pack
*
* Project home : https://github.com/loudenvier/kendo-global
* Kendo UI home : http://kendoui.com
* Author : Simba Mupfunya (solomupfunya@gmail.com)
*
*
* This project is released to the public domain, although one must abide to the
* licensing terms set forth by Telerik to use Kendo UI, as shown bellow.
*
* Telerik's original licensing terms:
* -----------------------------------
* Kendo UI Web commercial licenses may be obtained at
* https://www.kendoui.com/purchase/license-agreement/kendo-ui-web-commercial.aspx
* If you do not own a commercial license, this file shall be governed by the
* GNU General Public License (GPL) version 3.
* For GPL requirements, please review: http://www.gnu.org/copyleft/gpl.html
*/

kendo.ui.Locale = "VietNamses VN (vi-VN)";
kendo.ui.ColumnMenu.prototype.options.messages =
  $.extend(kendo.ui.ColumnMenu.prototype.options.messages, {

      /* COLUMN MENU MESSAGES
****************************************************************************/
      sortAscending: "Sắp xếp giảm dần",
      sortDescending: "Sắp xếp tăng dần",
      filter: "Lọc",
      columns: "Cột",
      done: "Done",
      settings: "Cấu hình cột"
      /***************************************************************************/
  });

kendo.ui.Groupable.prototype.options.messages =
  $.extend(kendo.ui.Groupable.prototype.options.messages, {

      /* GRID GROUP PANEL MESSAGES
****************************************************************************/
      empty: "Drag a column header and drop it here to group by that column"
      /***************************************************************************/
  });

kendo.ui.FilterMenu.prototype.options.messages =
  $.extend(kendo.ui.FilterMenu.prototype.options.messages, {

      /* FILTER MENU MESSAGES
***************************************************************************/
      info: "Điều kiện lọc :", // sets the text on top of the filter menu
      filter: "Lọc", // sets the text for the "Filter" button
      clear: "Xóa", // sets the text for the "Clear" button
      // when filtering boolean numbers
      isTrue: "Đúng", // sets the text for "isTrue" radio button
      isFalse: "Sai", // sets the text for "isFalse" radio button
      //changes the text of the "And" and "Or" of the filter menu
      and: "Và",
      or: "Hoặc",
      selectValue: "-Chọn giá trị-",
      operator: "Toán từ",
      value: "Giá trị",
      cancel: "Hủy bỏ"
      /***************************************************************************/
  });

kendo.ui.FilterMenu.prototype.options.operators =
  $.extend(kendo.ui.FilterMenu.prototype.options.operators, {

      /* FILTER MENU OPERATORS (for each supported data type)
****************************************************************************/
      string: {
          eq: "Bằng",
          neq: "Không bằng",
          startswith: "Bắt đầu",
          contains: "Chứa",
          doesnotcontain: "Không chứa",
          endswith: "Kết thúc với"
      },
      number: {
          eq: "Bằng",
          neq: "Không bằng",
          gte: "Lớn hơn hoặc bằng",
          gt: "Lớn hơn",
          lte: "Nhỏ hơn hoặc bằng",
          lt: "Nhỏ hơn"
      },
      date: {
          eq: "Bằng",
          neq: "Không bằng",
          gte: "Lớn hơn hoặc bằng",
          gt: "Lớn hơn",
          lte: "Nhỏ hơn hoặc bằng",
          lt: "Nhỏ hơn"
      },
      enums: {
          eq: "Bằng",
          neq: "Không bằng"
      }
      /***************************************************************************/
  });

kendo.ui.Pager.prototype.options.messages =
  $.extend(kendo.ui.Pager.prototype.options.messages, {

      /* PAGER MESSAGES
****************************************************************************/
      display: "{0} - {1} của {2} bản ghi",
      empty: "No items to display",
      page: "Trang",
      of: "của {0}",
      itemsPerPage: "items per page",
      first: "Đầu tiên",
      previous: "Trang trước",
      next: "Trang tiếp theo",
      last: "Cuối cùng",
      refresh: "Nạp lại"
      /***************************************************************************/
  });

kendo.ui.Validator.prototype.options.messages =
  $.extend(kendo.ui.Validator.prototype.options.messages, {

      /* VALIDATOR MESSAGES
****************************************************************************/
      required: "{0} is required",
      pattern: "{0} is not valid",
      min: "{0} should be greater than or equal to {1}",
      max: "{0} should be smaller than or equal to {1}",
      step: "{0} is not valid",
      email: "{0} is not valid email",
      url: "{0} is not valid URL",
      date: "{0} is not valid date"
      /***************************************************************************/
  });

kendo.ui.ImageBrowser.prototype.options.messages =
  $.extend(kendo.ui.ImageBrowser.prototype.options.messages, {

      /* IMAGE BROWSER MESSAGES
****************************************************************************/
      uploadFile: "Upload",
      orderBy: "Arrange by",
      orderByName: "Name",
      orderBySize: "Size",
      directoryNotFound: "A directory with this name was not found.",
      emptyFolder: "Empty Folder",
      deleteFile: 'Are you sure you want to delete "{0}"?',
      invalidFileType: "The selected file \"{0}\" is not valid. Supported file types are {1}.",
      overwriteFile: "A file with name \"{0}\" already exists in the current directory. Do you want to overwrite it?",
      dropFilesHere: "drop file here to upload",
      search: "Search"
      /***************************************************************************/
  });

kendo.ui.Editor.prototype.options.messages =
  $.extend(kendo.ui.Editor.prototype.options.messages, {

      /* EDITOR MESSAGES
****************************************************************************/
      bold: "Bold",
      italic: "Italic",
      underline: "Underline",
      strikethrough: "Strikethrough",
      superscript: "Superscript",
      subscript: "Subscript",
      justifyCenter: "Center text",
      justifyLeft: "Align text left",
      justifyRight: "Align text right",
      justifyFull: "Justify",
      insertUnorderedList: "Insert unordered list",
      insertOrderedList: "Insert ordered list",
      indent: "Indent",
      outdent: "Outdent",
      createLink: "Insert hyperlink",
      unlink: "Remove hyperlink",
      insertImage: "Insert image",
      insertHtml: "Insert HTML",
      viewHtml: "View HTML",
      fontName: "Select font family",
      fontNameInherit: "(inherited font)",
      fontSize: "Chọn cỡ chữ",
      fontSizeInherit: "(inherited size)",
      formatBlock: "Định dạng",
      formatting: "Định dạng",
      foreColor: "Màu",
      backColor: "Màu nền",
      style: "Kiểu",
      emptyFolder: "Empty Folder",
      uploadFile: "Upload",
      orderBy: "Arrange by:",
      orderBySize: "Kích thước",
      orderByName: "Tên",
      invalidFileType: "Tệp được chọn \"{0}\" có kiểu không hợp lệ. Vui lòng chọn tệp khác {1}.",
      deleteFile: 'Bạn muốn xóa phải không "{0}"?',
      overwriteFile: 'Tên tệp "{0}" Đã tồn tại trong thư mục. Bạn có muốn ghi đè không?',
      directoryNotFound: "Không tìm thấy thư mục mới tên này.",
      imageWebAddress: "Web address",
      imageAltText: "Alternate text",
      linkWebAddress: "Địa chỉ Web",
      linkText: "Text",
      linkToolTip: "ToolTip",
      linkOpenInNewWindow: "Mở cửa sổ mới",
      dialogUpdate: "Cập nhật",
      dialogInsert: "Chèn",
      dialogButtonSeparator: "hoặc",
      dialogCancel: "Hủy bỏ",
      createTable: "Tạo bảng",
      addColumnLeft: "Thêm cột bên trái",
      addColumnRight: "Thêm cột bên phải",
      addRowAbove: "Thêm dòng bên trên",
      addRowBelow: "Thêm dòng bên dưới",
      deleteRow: "Xóa dòng",
      deleteColumn: "Xóa cột"
      /***************************************************************************/
  });

kendo.ui.NumericTextBox.prototype.options =
    $.extend(kendo.ui.NumericTextBox.prototype.options, {

        /* NUMERIC TEXT BOX OR INTEGER TEXT BOX MESSAGES
****************************************************************************/
        upArrowText: "Tăng giá trị",
        downArrowText: "Giảm giá trị"
        /***************************************************************************/
    });

kendo.ui.Upload.prototype.options.localization =
$.extend(kendo.ui.Upload.prototype.options.localization, {

    /* UPLOAD LOCALIZATION
    ****************************************************************************/
    select: "Chọn tệp...",
    cancel: "Hủy bỏ",
    retry: "Thử lại",
    remove: "Xóa",
    uploadSelectedFiles: "Những tệp tải lên:",
    dropFilesHere: "Kéo tệp tin để tải lên web",
    statusUploading: "Bắt đầu tải lên",
    statusUploaded: "Đã tải",
    statusWarning: "Chờ",
    statusFailed: "Lỗi",
    headerStatusUploading: "Đang tải...",
    headerStatusUploaded: "Hoàn thành"
    /***************************************************************************/
});