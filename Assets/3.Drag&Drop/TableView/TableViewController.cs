using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(ScrollRect))]
public class TableViewController<T> : ViewController		// ViewController 클래스를 상속
{
    protected List<T> tableData = new List<T>();			// 리스트 항목의 데이터를 저장
    private Rect visibleRect;                               // 리스트 항목을 셀의 형태로 표시하는 범위를 나타내는 사각형
    private ScrollRect cachedScrollRect;                    // Scroll Rect 컴포넌트를 캐시한다
    private Vector2 prevScrollPos;	                        // 바로 전의 스크롤 위치를 저장
    private LinkedList<TableViewCell<T>> cells = new LinkedList<TableViewCell<T>>();         // 셀을 저장

    [SerializeField] private GameObject cellBase;   // 복사 원본 셀
    [SerializeField] private RectOffset visibleRectPadding; // visibleRect의 패딩
    [SerializeField] private RectOffset padding;			// 스크롤할 내용의 패딩
	[SerializeField] private float spacingHeight = 4.0f;	// 각 셀의 간격
    
    public ScrollRect CachedScrollRect
	{
		get {
			if(cachedScrollRect == null) { 
				cachedScrollRect = GetComponent<ScrollRect>(); }
			return cachedScrollRect;
		}
	}

    // 인스턴스를 로드할 때 호출된다
	protected virtual void Awake()
	{
	}

    // 리스트 항목에 대응하는 셀의 높이를 반환하는 메서드
	protected virtual float CellHeightAtIndex(int index)
	{
        // 실제 값을 반환하는 처리는 상속한 클래스에서 구현한다
		return 0.0f;
	}

    // 스크롤할 내용 전체의 높이를 갱신하는 메서드
	protected void UpdateContentSize()
	{
        // 스크롤할 내용 전체의 높이를 계산한다
		float contentHeight = 0.0f;
		for(int i=0; i<tableData.Count; i++)
		{
			contentHeight += CellHeightAtIndex(i);
			if(i > 0) { contentHeight += spacingHeight; }
		}

        // 스크롤할 내용의 높이를 설정한다
		Vector2 sizeDelta = CachedScrollRect.content.sizeDelta;
		sizeDelta.y = padding.top + contentHeight + padding.bottom;
		CachedScrollRect.content.sizeDelta = sizeDelta;
	}

    // 인스턴스를 로드할 때 Awake 메서드 다음에 호출된다
	protected virtual void Start()
	{
        // 복사 원본 셀은 비활성화해둔다
		cellBase.SetActive(false);

		// Scroll Rect 컴포넌트의 On Value Changed 이벤트의 이벤트 리스너를 설정한다
		CachedScrollRect.onValueChanged.AddListener(OnScrollPosChanged);
	}

    // 셀을 생성하는 메서드
	private TableViewCell<T> CreateCellForIndex(int index)
	{
        // 복사 원본 셀을 이용해 새로운 셀을 생성한다
		GameObject obj = Instantiate(cellBase) as GameObject;
		obj.SetActive(true);
		TableViewCell<T> cell = obj.GetComponent<TableViewCell<T>>();

        // 부모 요소를 바꾸면 스케일이나 크기를 잃어버리므로 변수에 저장해둔다
		Vector3 scale = cell.transform.localScale;
		Vector2 sizeDelta = cell.CachedRectTransform.sizeDelta;
		Vector2 offsetMin = cell.CachedRectTransform.offsetMin;
		Vector2 offsetMax = cell.CachedRectTransform.offsetMax;

		cell.transform.SetParent(cellBase.transform.parent);

        // 셀의 스케일과 크기를 설정한다
		cell.transform.localScale = scale;
		cell.CachedRectTransform.sizeDelta = sizeDelta;
		cell.CachedRectTransform.offsetMin = offsetMin;
		cell.CachedRectTransform.offsetMax = offsetMax;

        // 지정된 인덱스가 붙은 리스트 항목에 대응하는 셀로 내용을 갱신한다
		UpdateCellForIndex(cell, index);

		cells.AddLast(cell);

		return cell;
	}

    // 셀의 내용을 갱신하는 메서드
	private void UpdateCellForIndex(TableViewCell<T> cell, int index)
	{
        // 셀에 대응하는 리스트 항목의 인덱스를 설정한다
		cell.DataIndex = index;

		if(cell.DataIndex >= 0 && cell.DataIndex <= tableData.Count-1)
		{
            // 셀에 대응하는 리스트 항목이 있다면 셀을 활성화해서 내용을 갱신하고 높이를 설정한다
			cell.gameObject.SetActive(true);
			cell.UpdateContent(tableData[cell.DataIndex]);
			cell.Height = CellHeightAtIndex(cell.DataIndex);
		}
		else
		{
            // 셀에 대응하는 리스트 항목이 없다면 셀을 비활성화시켜 표시되지 않게 한다
			cell.gameObject.SetActive(false);
		}
	}

    // visibleRect을 갱신하기 위한 메서드
	private void UpdateVisibleRect()
	{
        // visibleRect의 위치는 스크롤할 내용의 기준으로부터 상대적인 위치다
		visibleRect.x = 
			CachedScrollRect.content.anchoredPosition.x + visibleRectPadding.left;
		visibleRect.y = 
			-CachedScrollRect.content.anchoredPosition.y + visibleRectPadding.top;

        // visibleRect의 크기는 스크롤 뷰의 크기 + 패딩
		visibleRect.width = CachedRectTransform.rect.width + 
			visibleRectPadding.left + visibleRectPadding.right;
		visibleRect.height = CachedRectTransform.rect.height + 
			visibleRectPadding.top + visibleRectPadding.bottom;
	}

	protected void UpdateContents()
	{
        UpdateContentSize();	// 스크롤할 내용의 크기를 갱신한다
        UpdateVisibleRect();	// visibleRect를 갱신한다

		if(cells.Count < 1)
		{
            // 셀이 하나도 없을 때는 visibleRect의 범위에 들어가는 첫 번째 리스트 항목을 찾아서
            // 그에 대응하는 셀을 작성한다
			Vector2 cellTop = new Vector2(0.0f, -padding.top);
			for(int i=0; i<tableData.Count; i++)
			{
				float cellHeight = CellHeightAtIndex(i);
				Vector2 cellBottom = cellTop + new Vector2(0.0f, -cellHeight);
				if((cellTop.y <= visibleRect.y && 
					cellTop.y >= visibleRect.y - visibleRect.height) || 
				   (cellBottom.y <= visibleRect.y && 
				   	cellBottom.y >= visibleRect.y - visibleRect.height))
				{
					TableViewCell<T> cell = CreateCellForIndex(i);
					cell.Top = cellTop;
					break;
				}
				cellTop = cellBottom + new Vector2(0.0f, spacingHeight);
			}

            // visibleRect의 범위에 빈 곳이 있으면 셀을 작성한다
			FillVisibleRectWithCells();
		}
		else
		{
            // 이미 셀이 있을 때는 첫 번째 셀부터 순서대로 대응하는 리스트 항목의
            // 인덱스를 다시 설정하고 위치와 내용을 갱신한다
			LinkedListNode<TableViewCell<T>> node = cells.First;
			UpdateCellForIndex(node.Value, node.Value.DataIndex);
			node = node.Next;
			
			while(node != null)
			{
				UpdateCellForIndex(node.Value, node.Previous.Value.DataIndex + 1);
				node.Value.Top = 
					node.Previous.Value.Bottom + new Vector2(0.0f, -spacingHeight);
				node = node.Next;
			}

            // visibleRect의 범위에 빈 곳이 있으면 셀을 작성한다
			FillVisibleRectWithCells();
		}
	}

    // visibleRect 범위에 표시될 만큼의 셀을 작성하는 메서드
	private void FillVisibleRectWithCells()
	{
		// 셀이 없다면 아무 일도 하지 않는다
		if(cells.Count < 1)
		{
			return;
		}

        // 표시된 마지막 셀에 대응하는 리스트 항목의 다음 리스트 항목이 있고
        // 또한 그 셀이 visibleRect의 범위에 들어온다면 대응하는 셀을 작성한다
		TableViewCell<T> lastCell = cells.Last.Value;
		int nextCellDataIndex = lastCell.DataIndex + 1;
		Vector2 nextCellTop = lastCell.Bottom + new Vector2(0.0f, -spacingHeight);

		while(nextCellDataIndex < tableData.Count && 
			nextCellTop.y >= visibleRect.y - visibleRect.height)
		{
			TableViewCell<T> cell = CreateCellForIndex(nextCellDataIndex);
			cell.Top = nextCellTop;

			lastCell = cell;
			nextCellDataIndex = lastCell.DataIndex + 1;
			nextCellTop = lastCell.Bottom + new Vector2(0.0f, -spacingHeight);
		}
	}

	// 스크롤 뷰가 스크롤됐을 때 호출된다
	public void OnScrollPosChanged(Vector2 scrollPos)
	{
		// visibleRect를 갱신한다
		UpdateVisibleRect();
        // 스크롤한 방향에 따라 셀을 다시 이용해 표시를 갱신한다
		ReuseCells((scrollPos.y < prevScrollPos.y)? 1: -1);

		prevScrollPos = scrollPos;
	}

    // 셀을 다시 이용해서 표시를 갱신하는 메서드
	private void ReuseCells(int scrollDirection)
	{
		if(cells.Count < 1)
		{
			return;
		}

		if(scrollDirection > 0)
		{
            // 위로 스크롤하고 있을 때는 visibleRect에 지정된 범위보다 위에 있는 셀을
            // 아래를 향해 순서대로 이동시켜 내용을 갱신한다
			TableViewCell<T> firstCell = cells.First.Value;
			while(firstCell.Bottom.y > visibleRect.y)
			{
				TableViewCell<T> lastCell = cells.Last.Value;
				UpdateCellForIndex(firstCell, lastCell.DataIndex + 1);
				firstCell.Top = lastCell.Bottom + new Vector2(0.0f, -spacingHeight);

				cells.AddLast(firstCell);
				cells.RemoveFirst();
				firstCell = cells.First.Value;
			}

            // visibleRect에 지정된 범위 안에 빈 곳이 있으면 셀을 작성한다
			FillVisibleRectWithCells();
		}
		else if(scrollDirection < 0)
		{
            // 아래로 스크롤하고 있을 때는 visibleRect에 지정된 범위보다 아래에 있는 셀을
            // 위를 향해 순서대로 이동시켜 내용을 갱신한다
			TableViewCell<T> lastCell = cells.Last.Value;
			while(lastCell.Top.y < visibleRect.y - visibleRect.height)
			{
				TableViewCell<T> firstCell = cells.First.Value;
				UpdateCellForIndex(lastCell, firstCell.DataIndex - 1);
				lastCell.Bottom = firstCell.Top + new Vector2(0.0f, spacingHeight);

				cells.AddFirst(lastCell);
				cells.RemoveLast();
				lastCell = cells.Last.Value;
			}
		}
	}
}
