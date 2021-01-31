using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class UIController : MonoBehaviour
{

    public enum OnHideAction
    {
        None,
        Disable,
        Destory,
    }
    public class PlayAsync : CustomYieldInstruction
    {

        public PlayAsync(UIController controller, bool isShow)
        {
            this.m_Obj = controller;
            this.m_ObjName = controller.ToString();

            if (isShow)
            {
                controller.Show(this.OnCompleted);
            }
            else
            {
                controller.Hide(this.OnCompleted);
            }
        }

        public override bool keepWaiting
        {
            get
            {
                if (this.m_Obj == null)
                {
                    throw new System.Exception(this.m_ObjName + " is be destroy, you can't keep waiting.");
                }
                return !this.m_IsDone;
            }
        }

        private UIController m_Obj;
        private string m_ObjName;
        private bool m_IsDone;

        private void OnCompleted()
        {
            this.m_IsDone = true;
        }
    }

    public bool onShowTurnOffAnimator = false;
    public bool onHideTurnOffAnimator = false;
    public bool showOnAwake = true;
    public OnHideAction onHideAction = OnHideAction.Disable;

    [SerializeField] private UnityEvent m_OnShow = new UnityEvent();
    [SerializeField] private UnityEvent m_OnHide = new UnityEvent();
    private UnityEvent m_OnShowDisposable = new UnityEvent();
    private UnityEvent m_OnHideDisposable = new UnityEvent();
    private Animator m_Animator;

    public UnityEvent onShow
    {
        get { return this.m_OnShow; }
        private set { this.m_OnShow = value; }
    }
    public UnityEvent onHide
    {
        get { return this.m_OnHide; }
        private set { this.m_OnHide = value; }
    }
    public bool isShow
    {
        get
        {
            if (this.animator.runtimeAnimatorController == null)
            {
                return this.gameObject.activeSelf;
            }
            if (!this.animator.isInitialized)
            {
                return false;
            }
            if (this.animator.gameObject.activeInHierarchy)
            {
                return this.animator.GetBool("Is Show");
            }
            return false;
        }
        private set
        {
            if (this.animator.runtimeAnimatorController == null)
            {
                if (value)
                {
                    this.OnShow();
                }
                else
                {
                    this.OnHide();
                }
                return;
            }
            if (this.animator.gameObject.activeInHierarchy)
            {
                this.animator.SetBool("Is Show", value);
                this.animator.SetTrigger("Play");
            }
        }
    }
    public bool isPlaying
    {
        get
        {
            if (!this.isValidController)
            {
                return false;
            }
            if (this.animator.gameObject.activeInHierarchy)
            {
                AnimatorStateInfo currentState = this.animator.GetCurrentAnimatorStateInfo(0);
                return (currentState.IsName("Show") || currentState.IsName("Hide"))
                    && currentState.normalizedTime < 1;
            }
            else
            {
                return false;
            }
        }
    }
    public Animator animator
    {
        get
        {
            if (this.m_Animator == null)
            {
                this.m_Animator = this.GetComponent<Animator>();
            }
            return this.m_Animator;
        }
    }
    private bool canTransitionToSelf
    {
        get
        {
            if (!this.isValidController)
            {
                return true;
            }
            if (this.animator.gameObject.activeInHierarchy)
                return this.animator.GetBool("Can Transition To Self");
            return true;
        }
    }
    private bool isValidController
    {
        get
        {
            return this.animator.runtimeAnimatorController != null && this.animator.isInitialized;
        }
    }

    // Show/Hide must fast by Show(UnityAction)Hide(UnityAction), make SendMessage("Show/Hide") working in Inspector
    public virtual void Show()
    {
        animator.enabled = true;
        if (!this.canTransitionToSelf && this.isShow)
        {
            if (!this.isPlaying)
            {
                this.OnShow();
            }
            return;
        }

        this.gameObject.SetActive(true);
        this.isShow = true;
    }
    public virtual void Hide()
    {
        animator.enabled = true;
        if (!this.canTransitionToSelf && !this.isShow)
        {
            if (!this.isPlaying)
            {
                this.OnHide();
            }
            return;
        }
        this.isShow = false;
    }
    public void Show(UnityAction onShow)
    {
        if (onShow != null)
        {
            m_OnShowDisposable.AddListener(onShow);
        }
        this.Show();
    }
    public void Hide(UnityAction onHide)
    {
        if (onHide != null)
        {
            m_OnHideDisposable.AddListener(onHide);
        }
        this.Hide();
    }
    public PlayAsync ShowAsync()
    {
        return new PlayAsync(this, true);
    }
    public PlayAsync HideAsync()
    {
        return new PlayAsync(this, false);
    }

    protected virtual void OnEnable()
    {
        //this.animator.Update(0);
        if (this.showOnAwake)
        {
            this.Show();
        }
    }
    protected virtual void OnShow()
    {
        this.onShow.Invoke();
        this.m_OnShowDisposable.Invoke();
        this.m_OnShowDisposable.RemoveAllListeners();
        if (onShowTurnOffAnimator)
        {
            animator.enabled = false;
        }
    }
    protected virtual void OnHide()
    {
        if (!this.isValidController || !this.isShow)
        {
            switch (this.onHideAction)
            {
                case UIController.OnHideAction.None:
                    if( onHideTurnOffAnimator ) {
                        animator.enabled = false;
                    }
                    break;
                case UIController.OnHideAction.Disable:
                    this.gameObject.SetActive(false);
                    break;
                case UIController.OnHideAction.Destory:
                    Destroy(this.gameObject);
                    break;
            }
        }
        this.onHide.Invoke();
        this.m_OnHideDisposable.Invoke();
        this.m_OnHideDisposable.RemoveAllListeners();
    }
}
